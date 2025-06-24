namespace Mail.Engine.Service.Core.Entities
{
    public class CustomerEntity
    {
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public bool IsAccountActive { get; set; }
        public string PhoneNumber { get; set; }

        public Guid? CountryId { get; set; }
        public string CountryName { get; set; }
        public string Alpha3Code { get; set; }
        public string MobileCode { get; set; }
        public string MobileRegex { get; set; }
        public string TimeZone { get; set; }
    }
}
