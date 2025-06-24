namespace Mail.Engine.Service.Core.Entities
{
    public class CustomerSessionEntity
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public string SessionToken { get; set; }
        public string BearerSessionToken { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; } = null;
        public string SessionHashKey { get; set; }
        public DateTime LastActiveTime { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public bool? IsAutoLogout { get; set; }
    }
}
