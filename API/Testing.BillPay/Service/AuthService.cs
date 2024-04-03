

using Testing.BillPay.Models.Dto;
using Testing.BillPay.Service.IService;
using Testing.BillPay.Utility;

namespace Testing.BillPay.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = SD.AuthAPIBase + "/login"
            }, withBearer: false);
        }

    }
}

