namespace Testing.DanamonNew.Models
{
    public class RegistrationVARequest
    {
        public string UserReferenceNumber { get; set; }
        public string RequestTime { get; set; }
        public string VirtualAccountNumber { get; set; }
        public string VirtualAccountName { get; set; }
        public string VirtualAccountExpiryDate { get; set; }
    }
}
