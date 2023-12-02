using Testing.BillPay.Models.Dto;

namespace Testing.BillPay.Service.IService
{
    public interface IDanamonService
    {
        Task<ResponseDto?> LoginAuthAsync(DanamonAuthDto loginRequestDto);

        Task<ResponseDto?> RegistrationVAAsync(RegistrationVARequestDto registrationVaRequestDto);

        Task<ResponseDto?> AccountInquiryBalanceAsync(AccountInquiryBalanceRequestDto accountInquiryBalanceRequestDto);
    }
}
