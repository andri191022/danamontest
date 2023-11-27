using Testing.DanamonNew.Models.Dto;

namespace Testing.DanamonNew.Service.IService
{
    public interface IDanamonService
    {
        Task<ResponseDto?> LoginAuthAsync(DanamonAuthDto loginRequestDto);

        Task<ResponseDto?> RegistrationVAAsync(RegistrationVARequestDto registrationVaRequestDto);

        Task<ResponseDto?>AccountInquiryBalanceAsync(AccountInquiryBalanceRequestDto accountInquiryBalanceRequestDto);
    }
}
