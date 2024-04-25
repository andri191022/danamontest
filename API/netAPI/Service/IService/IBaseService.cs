using netAPI.Models.Dto;

namespace netAPI.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);

        Task<ResponseDto?> postAsync(RequestDto requestDto);
    }
}
