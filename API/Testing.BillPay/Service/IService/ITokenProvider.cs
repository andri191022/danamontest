namespace Testing.BillPay.Service.IService
{
    public interface ITokenProvider
    {
        void SetToken(string token);
        void SetAuthToken(string token);
        string? GetToken();
        void ClearToken();
    }
}
