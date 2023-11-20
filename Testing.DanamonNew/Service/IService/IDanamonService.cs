using Testing.DanamonNew.Models;

namespace Testing.DanamonNew.Service.IService
{
    public interface IDanamonService
    {
        Task<ResponseDto?> LoginAuthAsync(DanamonAuthDto loginRequestDto);

        Task<ResponseDto?> RegistrationVAAsync(RegistrationVARequestDto registrationVaRequestDto);
    }
}
