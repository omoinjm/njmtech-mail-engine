namespace Mail.Engine.Service.Function.Exceptions
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
