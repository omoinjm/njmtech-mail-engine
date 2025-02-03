namespace Mail.Engine.Service.Application.Response.General
{
    public class CreateResponse
    {
        public int? ReturnRecordId { get; set; } = 0;
        public bool IsSuccess { get; set; } = false;
        public string Error { get; set; } = string.Empty;
    }
}