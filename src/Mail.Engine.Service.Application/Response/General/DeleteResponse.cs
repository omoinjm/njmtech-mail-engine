namespace Mail.Engine.Service.Application.Response.General
{
    public class DeleteResponse
    {
        public bool IsSuccess { get; set; } = false;
        public string Error { get; set; } = string.Empty;
    }
}