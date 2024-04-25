using netAPI.Models.Dto;

namespace netAPI.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
    }
}
