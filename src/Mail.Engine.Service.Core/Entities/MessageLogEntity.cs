namespace Mail.Engine.Service.Core.Entities
{
    public class MessageLogEntity
    {
        public Guid MessageLogId { get; set; }
        public Guid MessageLogHeaderId { get; set; }
        public System.String? FromField { get; set; }
        public System.String? FromName { get; set; }
        public System.String? ToField { get; set; }
        public System.String? CcField { get; set; }
        public System.String? BccField { get; set; }
        public System.String? Subject { get; set; }
        public System.String? Body { get; set; }
        public Guid MessageLogTypeId { get; set; }
        public Guid MessageLogStatusId { get; set; }
        public System.DateTime? DateSent { get; set; }
        public System.String? StatusMessage { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.Int32 CreatedBy { get; set; }
        public System.String? MessageLogTypeName { get; set; }
        public System.String? MessageLogTypeCode { get; set; }
        public System.String? MessageLogStatusName { get; set; }
        public System.String? MessageLogStatusCode { get; set; }
        public System.String? SmtpUserName { get; set; }
        public Guid SmtpConfigurationId { get; set; }
        public System.String? Smtp { get; set; }
        public System.Boolean? SSL { get; set; }
        public System.Int32? Port { get; set; }
        public System.String? SmtpEmailAddress { get; set; }
        public System.String? SmtpPassword { get; set; }
    }
}