namespace Testing.BillPay.Models.Dto
{
    public class AuthResponseB2BDto
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string accessToken { get; set; }
        public string expiresIn { get; set; }
        public string tokenType { get; set; }
    }
}
