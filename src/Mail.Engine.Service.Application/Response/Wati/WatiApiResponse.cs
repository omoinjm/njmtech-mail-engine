namespace Mail.Engine.Service.Application.Response.Wati
{
    public class WatiApiResponse
    {
        public bool Result { get; set; }
        public string? PhoneNumber { get; set; }
        public string? TemplateName { get; set; }
        public List<ParameterResponse>? Params { get; set; }
        public ContactResponse? Contact { get; set; }
        public ModelResponse? Model { get; set; }
        public bool ValidWhatsAppNumber { get; set; }
    }
}
