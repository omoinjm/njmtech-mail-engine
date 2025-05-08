using Newtonsoft.Json;

namespace Mail.Engine.Service.Core.Results.Wati
{
    public class ParameterResult
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("value")]
        public string? Value { get; set; }
    }
}
