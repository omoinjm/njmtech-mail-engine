namespace Mail.Engine.Service.Application.Dto
{
    public class MessageLogDto
    {
        public Guid MessageLogTypeId { get; set; }
        public string Subject { get; set; }
        public string ToField { get; set; }
        public string Body { get; set; }
        public string? FromName { get; set; }
    }
}
