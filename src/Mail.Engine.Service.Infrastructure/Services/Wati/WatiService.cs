using System.Net.Http.Headers;
using System.Text;
using Mail.Engine.Service.Core.Entities;
using Mail.Engine.Service.Core.Enum;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Results.Wati;
using Mail.Engine.Service.Core.Services.Wati;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mail.Engine.Service.Infrastructure.Services.Wati
{
    public class WatiService : IWatiService
    {
        private readonly HttpClient _httpClient = new();
        private readonly IWatiRepository _watiRepository;
        private readonly IMailRepository _mailRepository;

        public WatiService(IWatiRepository watiRepository, IMailRepository mailRepository)
        {
            _watiRepository = watiRepository;
            _mailRepository = mailRepository;

            var watiConfig = _watiRepository.GetWatiConfig().GetAwaiter().GetResult(); ;

            if (watiConfig != null)
            {
                _httpClient.BaseAddress = new Uri(watiConfig.BaseUrl);

                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", watiConfig.Bearer);
            }
        }


        public async Task<WatiApiResult> SendMessageTemplate(string whatsappNumber, string payload)
        {
            // await InitializeHttpClient();

            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"sendTemplateMessage?whatsappNumber={whatsappNumber}", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<WatiApiResult>(responseContent)!;
        }

        public async Task<WatiApiResult> SendMessage(string whatsappNumber, string message)
        {
            // await InitializeHttpClient();

            var response = await _httpClient.PostAsync($"sendSessionMessage/{whatsappNumber}?messageText={message}", null);

            response.EnsureSuccessStatusCode();

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

            if (messageLog!.MessageLogId.HasValue)
            {
                await _watiRepository.InsertJsonData(messageLog.MessageLogId.Value, JsonConvert.SerializeObject(result));
            }
        }

        private async Task InitializeHttpClient()
        {
            var watiConfig = await _watiRepository.GetWatiConfig();

            if (watiConfig != null)
            {
                _httpClient.BaseAddress = new Uri(watiConfig.BaseUrl);

                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", watiConfig.Bearer);
            }
        }
    }
}
