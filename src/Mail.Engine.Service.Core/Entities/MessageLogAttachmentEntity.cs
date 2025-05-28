namespace Mail.Engine.Service.Core.Entities
{
    public class MessageLogAttachmentEntity
    {
        public System.Int32 MessageLogAttachmentId { get; set; }
        public System.Int32 MessageLogId { get; set; }
        public System.String? AttachmentUrl { get; set; }
        public System.String? FileName { get; set; }
        public System.String? ContentId { get; set; }
        public System.Boolean? IsInlineImage { get; set; }
        public System.Byte[]? AttachmentData { get; set; }
    }
}
