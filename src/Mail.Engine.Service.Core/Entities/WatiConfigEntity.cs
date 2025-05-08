namespace Mail.Engine.Service.Core.Entities
{
    public class WatiConfigEntity
    {
        public Guid RecordId { get; set; }
        public string Bearer { get; set; }
        public string BaseUrl { get; set; }
        public int ClientId { get; set; }
    }
}
