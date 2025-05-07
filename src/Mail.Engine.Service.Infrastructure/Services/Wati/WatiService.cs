using System.Net.Http.Headers;
using System.Text;
using Mail.Engine.Service.Core.Repositories;
using Mail.Engine.Service.Core.Results.Wati;
using Mail.Engine.Service.Core.Services.Wati;
using Newtonsoft.Json;

namespace Mail.Engine.Service.Infrastructure.Services.Wati
{
    public class WatiService(IWatiRepository watiRepository) : IWatiService
    {
        private readonly HttpClient _httpClient;
        private readonly IWatiRepository _watiRepository = watiRepository;

        public async Task<WatiApiResult> SendMessage(string whatsappNumber, object payload)
        {
            await InitializeHttpClient();

            var jsonString = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"sendTemplateMessage?whatsappNumber={whatsappNumber}", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<WatiApiResult>(responseContent)!;
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
