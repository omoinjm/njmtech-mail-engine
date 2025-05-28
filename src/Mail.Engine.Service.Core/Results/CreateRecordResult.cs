namespace Mail.Engine.Service.Core.Results
{
    public class CreateRecordResult
    {
        public Guid ReturnRecordId { get; set; }
        public bool IsSuccess { get; set; }
        public string? ResponseMessage { get; set; }

        public CreateRecordResult() { }

        public static CreateRecordResult Successs(Guid id, string message)
        {
            return new CreateRecordResult() { IsSuccess = true, ReturnRecordId = id, ResponseMessage = message };
        }

        public static CreateRecordResult Error(string message)
        {
            return new CreateRecordResult() { IsSuccess = false, ResponseMessage = message };
        }
    }
}
