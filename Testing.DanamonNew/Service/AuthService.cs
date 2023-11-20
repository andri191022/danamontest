using Testing.DanamonNew.Models;
using Testing.DanamonNew.Service.IService;
using Testing.DanamonNew.Utility;

namespace Testing.DanamonNew.Service
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
