namespace Mail.Engine.Service.Application.Response
{
    public class MessageResponse
    {
        public Guid MailMessageId { get; set; }
        public Guid MailboxId { get; set; }
        public System.String? Subject { get; set; }
        public System.String? TextPlain { get; set; }
        public System.String? TextHtml { get; set; }
        public System.String? FormatTextHtml { get; set; }
        public System.DateTime? DateSent { get; set; }
        public System.String? CcField { get; set; }
        public System.String? BccField { get; set; }
        public System.String? SentTo { get; set; }
        public System.String? SentFrom { get; set; }
        public System.String? SentFromDisplayName { get; set; }
        public System.String? SentFromRaw { get; set; }
        public System.String? ImapMessageId { get; set; }
        public System.String? MimeVersion { get; set; }
        public System.String? ReturnPath { get; set; }
        public System.Int32? CreatedBy { get; set; }
        public System.DateTime? CreatedAt { get; set; }
        public System.Boolean? IsDeleted { get; set; }
    }
}