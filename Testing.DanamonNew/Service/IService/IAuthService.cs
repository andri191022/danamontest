using Testing.DanamonNew.Models;

namespace Testing.DanamonNew.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);

        
    }
}
