using Newtonsoft.Json;

namespace Mail.Engine.Service.Core.Results.Wati
{
    public class ModelResult
    {
        [JsonProperty("ids")]
        public List<string>? Ids { get; set; }
    }
}
