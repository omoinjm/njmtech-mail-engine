namespace Mail.Engine.Service.Core.Results.Wati
{
    public class WatiApiResult
    {
        public bool Result { get; set; }
        public string? PhoneNumber { get; set; }
        public string? TemplateName { get; set; }
        public List<ParameterResult>? Params { get; set; }
        public ContactResult? Contact { get; set; }
        public ModelResult? Model { get; set; }
        public bool ValidWhatsAppNumber { get; set; }
    }
}
