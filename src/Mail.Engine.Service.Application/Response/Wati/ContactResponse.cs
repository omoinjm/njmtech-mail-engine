namespace Mail.Engine.Service.Application.Response.Wati
{
    public class ContactResponse
    {
        public string? Id { get; set; }
        public string? TenantId { get; set; }
        public string? Waid { get; set; }
        public string? FirstName { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public object? Source { get; set; }
        public string? ContactStatus { get; set; }
        public object? Photo { get; set; }
        public string? Created { get; set; }
        public List<ParameterResponse>? CustomParams { get; set; }
        public bool OptedIn { get; set; }
        public bool IsDeleted { get; set; }
        public string? LastUpdated { get; set; }
        public bool AllowBroadcast { get; set; }
        public bool AllowSMS { get; set; }
        public List<string>? TeamIds { get; set; }
        public bool IsInFlow { get; set; }
        public string? LastFlowId { get; set; }
        public string? CurrentFlowNodeId { get; set; }
        public string? SelectedHubspotId { get; set; }
    }
}
