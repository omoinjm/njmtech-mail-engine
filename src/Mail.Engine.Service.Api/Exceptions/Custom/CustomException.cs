namespace Mail.Engine.Service.Api.Exceptions.Custom
{
    public class CustomException : Exception
    {
        public string? AdditionalInfo { get; set; }
        public string? Type { get; set; }
        public string? Detail { get; set; }
        public string? Title { get; set; }
        public string? Instance { get; set; }
    }
}