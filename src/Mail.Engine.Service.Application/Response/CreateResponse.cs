namespace Mail.Engine.Service.Application.Response
{
    public class CreateResponse
    {
        public Guid ReturnRecordId { get; set; }
        public bool IsSuccess { get; set; }
        public string? ResponseMessage { get; set; }

        public CreateResponse() { }

        public static CreateResponse Successs(Guid id, string message)
        {
            return new CreateResponse() { IsSuccess = true, ReturnRecordId = id, ResponseMessage = message };
        }

        public static CreateResponse Error(string message)
        {
            return new CreateResponse() { IsSuccess = false, ResponseMessage = message };
        }
    }
}
