namespace Mail.Engine.Service.Application.Response
{
    public class MailResponse
    {
        public int TotalMessagesProcessed { get; set; }
        public int TotalMessagesFailed { get; set; }
        public List<string> ErrorMessages { get; set; } = [];
        public bool IsSuccess => TotalMessagesFailed == 0;
    }
}