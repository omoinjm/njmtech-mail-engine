namespace Mail.Engine.Service.Core.Results
{
    public class UpdateRecordResult
    {
        public bool IsSuccess { get; set; } = false;
        public string Error { get; set; } = string.Empty;
    }
}