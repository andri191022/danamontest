using Testing.BillPay.Models.Dto;

namespace Testing.BillPay.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
    }
}
