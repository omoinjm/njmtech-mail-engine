using System.Text.Json;
using Mail.Engine.Service.Application.Queries;
using MediatR;

namespace Mail.Engine.Service.Api.Services
{
    public class RecurringTaskService(ILogger<RecurringTaskService> logger, IServiceScopeFactory serviceScopeFactory) : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
        private readonly ILogger<RecurringTaskService> _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RecurringTaskService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    // Call Inbound and Outbound queries
                    var inboundResult = await mediator.Send(new GetInboundQuery(), stoppingToken);
                    var outboundResult = await mediator.Send(new GetOutboundQuery(), stoppingToken);

                    // Call Wati query
                    var watiResult = await mediator.Send(new GetWatiQuery(), stoppingToken);

                    // var customerLogoutResult = await mediator.Send(new GetCustomerLogoutQuery(), stoppingToken);

                    _logger.LogInformation($"Inbound Mails Processed: {JsonSerializer.Serialize(inboundResult)}");
                    _logger.LogInformation($"Outbound Mails Processed: {JsonSerializer.Serialize(outboundResult)}");

                    _logger.LogInformation($"Wati Mails Processed: {JsonSerializer.Serialize(watiResult)}");

                    // _logger.LogInformation($"Wati Customer Autologout: {JsonSerializer.Serialize(customerLogoutResult)}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error executing recurring tasks.");
                }

                await Task.Delay(5000, stoppingToken); // Run every second
            }
        }
    }
}
