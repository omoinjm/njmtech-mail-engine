using System.Net.Http.Headers;
using System.Text;
using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Enum;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Results.Wati;
using Mail.Engine.Service.Core.Services.Wati;
using Newtonsoft.Json;

namespace Mail.Engine.Service.Infrastructure.Services.Wati
{
    public class WatiService(
        IWatiRepository watiRepository,
        IMailRepository mailRepository
    ) : IWatiService
    {
        private readonly HttpClient _httpClient = new();
        private readonly IWatiRepository _watiRepository = watiRepository;
        private readonly IMailRepository _mailRepository = mailRepository;


        public async Task<WatiApiResult> SendMessage(string whatsappNumber, string payload)
        {
            await InitializeHttpClient();

            // var jsonString = JsonConvert.SerializeObject(payload);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"sendTemplateMessage?whatsappNumber={whatsappNumber}", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<WatiApiResult>(responseContent)!;
        }

        public async Task UpdateMessageStatusAsync(MessageLogEntity messageLog, WatiApiResult result)
        {
            messageLog.StatusMessage = "Failed to send";
            messageLog.MessageLogStatusCode = EnumMessageStatusLog.Failed;

            if (result != null && messageLog != null)
            {
                messageLog.StatusMessage = "Successful";
                messageLog.DateSent = DateTime.Now;
                messageLog.MessageLogStatusCode = EnumMessageStatusLog.Sent;
            }

            await _mailRepository.UpdateStatusAsync(messageLog!);

            await _watiRepository.InsertJsonData(messageLog!.MessageLogId, JsonConvert.SerializeObject(result));
        }

        private async Task InitializeHttpClient()
        {
            var watiConfig = await _watiRepository.GetWatiConfig();

            if (watiConfig != null)
            {
                _httpClient.BaseAddress = new Uri(watiConfig.BaseUrl);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", watiConfig.Bearer);
            }
        }
    }
}
