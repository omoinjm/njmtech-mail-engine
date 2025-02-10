namespace Mail.Engine.Service.Core.Results
{
    public class MailResult
    {
        public int TotalMessagesProcessed { get; set; }
        public int TotalMessagesFailed { get; set; }
        public List<string> ErrorMessages { get; set; } = [];
        public bool IsSuccess => TotalMessagesFailed == 0;
    }
}