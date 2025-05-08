using Newtonsoft.Json;

namespace Mail.Engine.Service.Core.Results.Wati
{
    public class ContactResult
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("tenantId")]
        public string? TenantId { get; set; }

        [JsonProperty("wAid")]
        public string? WAid { get; set; }

        [JsonProperty("firstName")]
        public string? FirstName { get; set; }

        [JsonProperty("fullName")]
        public string? FullName { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; }

        [JsonProperty("source")]
        public string? Source { get; set; }

        [JsonProperty("contactStatus")]
        public string? ContactStatus { get; set; }

        [JsonProperty("photo")]
        public string? Photo { get; set; }

        [JsonProperty("created")]
        public string? Created { get; set; }

        [JsonProperty("customParams")]
        public List<ParameterResult>? CustomParams { get; set; }

        [JsonProperty("optedIn")]
        public bool OptedIn { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("lastUpdated")]
        public string? LastUpdated { get; set; }

        [JsonProperty("allowBroadcast")]
        public bool AllowBroadcast { get; set; }

        [JsonProperty("allowSMS")]
        public bool AllowSMS { get; set; }

        [JsonProperty("teamIds")]
        public List<string>? TeamIds { get; set; }

        [JsonProperty("isInFlow")]
        public bool IsInFlow { get; set; }

        [JsonProperty("isInTestFlow")]
        public bool IsInTestFlow { get; set; }

        [JsonProperty("lastFlowId")]
        public string? LastFlowId { get; set; }

        [JsonProperty("currentFlowNodeId")]
        public string? CurrentFlowNodeId { get; set; }

        [JsonProperty("selectedHubspotId")]
        public string? SelectedHubspotId { get; set; }

        [JsonProperty("channelId")]
        public string? ChannelId { get; set; }

        [JsonProperty("displayId")]
        public string? DisplayId { get; set; }

        [JsonProperty("contactLinkId")]
        public string? ContactLinkId { get; set; }

        [JsonProperty("waChannelPhone")]
        public string? WaChannelPhone { get; set; }

        [JsonProperty("igPhoneSource")]
        public int IgPhoneSource { get; set; }

        [JsonProperty("mgPhoneSource")]
        public int MgPhoneSource { get; set; }

        [JsonProperty("tagName")]
        public string? TagName { get; set; }

        [JsonProperty("messengerPageName")]
        public string? MessengerPageName { get; set; }

        [JsonProperty("paylinkSettings")]
        public object? PaylinkSettings { get; set; }

        [JsonProperty("ctwaFollowUpStatus")]
        public int CtwaFollowUpStatus { get; set; }

        [JsonProperty("ctwaFollowUpCount")]
        public int CtwaFollowUpCount { get; set; }

        [JsonProperty("ctwaFollowUpNotice")]
        public int CtwaFollowUpNotice { get; set; }
    }

}
