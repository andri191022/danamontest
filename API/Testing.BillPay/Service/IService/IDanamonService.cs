using Testing.BillPay.Models.Dto;

namespace Testing.BillPay.Service.IService
{
    public interface IDanamonService
    {
        Task<ResponseDto?> LoginAuthAsync(DanamonAuthDto loginRequestDto);
        Task<ResponseDto?> LoginAuth02Async(Auth02Dto loginRequestDto);
        Task<ResponseDto?> LoginAuthB2BAsync();

        Task<ResponseDto?> RegistrationVAAsync(RegistrationVARequestDto registrationVaRequestDto);

        Task<ResponseDto?> AccountInquiryBalanceAsync(AccountInquiryBalanceRequestDto accountInquiryBalanceRequestDto);
    }
}
