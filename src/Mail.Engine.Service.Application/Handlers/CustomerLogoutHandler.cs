using System.Linq;
using Mail.Engine.Service.Application.Helpers;
using Mail.Engine.Service.Application.Queries;
using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Helpers;
using Mail.Engine.Service.Core.Helpers.Constants;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Templates;
using MediatR;

namespace Mail.Engine.Service.Application.Handlers
{
    public class CustomerLogoutHandler(
        ICustomerRepository customerRepository,
        IMailRepository mailRepository
    ) : IRequestHandler<GetCustomerLogoutQuery, object>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IMailRepository _mailRepository = mailRepository;

        private readonly SemaphoreSlim _semaphore = new(10);

        public async Task<object?> Handle(GetCustomerLogoutQuery request, CancellationToken cancellationToken)
        {
            var responses = new List<object>();
            var customers = await _customerRepository.GetCustomerIdList();

            async Task AddResponse(string customerId, string phone, bool performed, string msg, string? err = null)
            {
                responses.Add(new
                {
                    CustomerId = customerId,
                    PhoneNumber = phone,
                    LogoutPerformed = performed,
                    Message = msg,
                    Error = err
                });
            }

            async Task<bool> SendMessage(string phone, Func<string> getBody, string subject = "Wati Service")
            {
                var result = await _mailRepository.CreateMessageLogRecord(new MessageLogEntity
                {
                    MessageLogTypeId = MessageLogConstants.WATI_ID,
                    ToField = phone,
                    Subject = subject,
                    Body = getBody()
                });

                return result.IsSuccess;
            }

            var tasks = customers.Select(async customer =>
            {
                await _semaphore.WaitAsync(); // Limit concurrency by waiting for the semaphore.

                try
                {
                    var sessions = await _customerRepository.GetCustomerSession(customer.CustomerId);

                    foreach (var session in sessions.Where(s => s.IsActive))
                    {
                        var token = session.SessionToken;
                        var phone = customer.PhoneNumber;

                        if (token == null || !phone.IsValidPhoneNumber(customer.Alpha3Code))
                        {
                            await AddResponse(customer.CustomerId, phone, false,
                                "Skipped due to invalid token or phone number format",
                                $"Invalid token or phone number format for country code: {customer.Alpha3Code}");
                            continue;
                        }

                        var idleMinutes = (DateTime.UtcNow - session.LastActiveTime).TotalMinutes;
                        if (JwtClaimsHelper.IsJwtTokenExpired(token) || idleMinutes > 15)
                        {
                            var loginKey = await _customerRepository.GetLoginKey(customer.CustomerId);
                            if (loginKey != null)
                                await _customerRepository.RemoveLoginKey(loginKey.Key);

                            await _customerRepository.UpdateCustomerSession(session.Id);

                            if (!await SendMessage(phone, () => PayloadTemplates.SendLoginTemplate(phone)))
                            {
                                await AddResponse(customer.CustomerId, phone, false,
                                    "Failed to send login template message", "Login template message failed");
                                continue;
                            }

                            await Task.Delay(TimeSpan.FromSeconds(3));

                            if (!await SendMessage(phone, () => PayloadTemplates.SendConfirmationMessage(
                                "You have been logged out of your Wallety account automatically due to inactivity, please login below."))
                            )
                            {
                                await AddResponse(customer.CustomerId, phone, false,
                                    "Failed to send confirmation message", "Confirmation message failed");
                                continue;
                            }

                            await AddResponse(customer.CustomerId, phone, true, "Logout performed and confirmation sent");
                        }
                    }
                }
                finally
                {
                    _semaphore.Release(); // Release the semaphore to allow other operations.
                }
            });

            await Task.WhenAll(tasks);

            return new
            {
                Timestamp = DateTime.UtcNow,
                TotalCustomersChecked = customers.Count,
                Results = responses
            };
        }
    }
}
