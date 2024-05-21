using Testing.BillPay.Models.Dto;

namespace Testing.BillPay.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);

        Task<ResponseDto?> postAsync(RequestDto requestDto);
        Task<ResponseDto?> postB2BAsync(RequestDto requestDto);
    }
}
