namespace Mail.Engine.Service.Core.Results
{
    public class CreateRecordResult
    {
        public int? ReturnRecordId { get; set; } = 0;
        public bool IsSuccess { get; set; } = false;
        public string Error { get; set; } = string.Empty;
    }
}