using Testing.DanamonNew.Models.Dto;

namespace Testing.DanamonNew.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);

        
    }
}
