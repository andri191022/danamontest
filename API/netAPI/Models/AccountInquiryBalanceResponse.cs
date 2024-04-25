namespace Testing.BillPay.Models
{
    public class AccountInquiryBalanceResponse
    {
        public string CodeStatus { get; set; }
        public string DescriptionStatus { get; set; }
        public string UserReferenceNumber { get; set; }
        public string ResponseTime { get; set; }
        public string AccountCurrency { get; set; }
        public string CurrentBalance { get; set; }
        public string AvailableBalance { get; set; }
        public string OverDraftLimit { get; set; }
        public string HoldAmount { get; set; }
        public string MinimumBalance { get; set; }
        public string BeginningBalance { get; set; }
        public string EndingBalance { get; set; }
    }
}
