namespace Mail.Engine.Service.Application.Response.General
{
    public class UpdateResponse
    {
        public bool IsSuccess { get; set; } = false;
        public string Error { get; set; } = string.Empty;
    }
}