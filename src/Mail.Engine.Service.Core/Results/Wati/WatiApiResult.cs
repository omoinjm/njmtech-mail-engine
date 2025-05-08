using Newtonsoft.Json;

namespace Mail.Engine.Service.Core.Results.Wati
{
    public class WatiApiResult
    {
        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("phone_number")]
        public string? PhoneNumber { get; set; }

        [JsonProperty("template_name")]
        public string? TemplateName { get; set; }

        [JsonProperty("parameteres")]
        public List<ParameterResult>? Params { get; set; }

        [JsonProperty("contact")]
        public ContactResult? Contact { get; set; }

        [JsonProperty("model")]
        public ModelResult? Model { get; set; }

        [JsonProperty("validWhatsAppNumber")]
        public bool ValidWhatsAppNumber { get; set; }
    }
}
