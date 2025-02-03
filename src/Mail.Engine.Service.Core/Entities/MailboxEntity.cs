namespace Mail.Engine.Service.Core.Entities
{
    public class MailboxEntity
    {
        public Guid MailboxId { get; set; }
        public System.String Name { get; set; }
        public Guid ImapId { get; set; }
        public Guid SmtpId { get; set; }
        public System.Boolean? IsActive { get; set; }
        public System.String ImapName { get; set; }
        public System.String Imap { get; set; }
        public System.Int32 ImapPort { get; set; }
        public System.Boolean ImapSsl { get; set; }
        public System.String ImapUsername { get; set; }
        public System.String ImapPassword { get; set; }
        public System.String SmtpName { get; set; }
        public System.String Smtp { get; set; }
        public System.Int32? SmtpPort { get; set; }
        public System.Boolean? SmtpSsl { get; set; }
        public System.String SmtpUsername { get; set; }
        public System.String SmtpPassword { get; set; }
        public System.Boolean? IsOutlook { get; set; }
        public System.String AppId { get; set; }
        public System.String TenantId { get; set; }
        public System.String SecretId { get; set; }
        public System.String AccessToken { get; set; }
        public System.Boolean? UseAccessToken { get; set; }
        public System.String CodeChallange { get; set; }
        public System.String RefreshToken { get; set; }
    }
}