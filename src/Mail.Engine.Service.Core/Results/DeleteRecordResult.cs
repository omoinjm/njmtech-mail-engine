namespace Mail.Engine.Service.Core.Results
{
    public class DeleteRecordResult
    {
        public bool IsSuccess { get; set; } = false;
        public string Error { get; set; } = string.Empty;
    }
}