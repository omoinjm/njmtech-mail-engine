namespace Mail.Engine.Service.Core.Entities
{
    public class LoginKeyEntity
    {
        public string Key { get; set; }
        public int LoginAttempts { get; set; }
        public DateTime TimeGenerated { get; set; }
        public string WalletyUserId { get; set; }
    }
}
