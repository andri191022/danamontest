namespace Testing.DanamonNew.Models.Dto
{
    public class RegistrationVARequestDto
    {
        public RegistrationVARequest registrationVARequest { get; set; }
        public string BDISignature { get; set; }
        public string BDIKey { get; set; }
        public string BDITimestamp { get; set; }
        public string Authorization { get; set; }
    }
}
