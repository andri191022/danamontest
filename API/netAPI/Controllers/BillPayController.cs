using Microsoft.AspNetCore.Mvc;
using netAPI.Models.Dto;
using Newtonsoft.Json;
using netAPI.Models;
using netAPI.Models.Dto;
using netAPI.Service.IService;

namespace netAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BillPayController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        private readonly IDanamonService _danamonService;


        public BillPayController(IAuthService authService, ITokenProvider tokenProvider, IDanamonService danamonService)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
            _danamonService = danamonService;
        }

        [HttpPost]
        public async Task<AuthResponseDto> GetAuth02([FromBody] Auth02Dto obj)
        {
            AuthResponseDto authResponseDto = new AuthResponseDto();

            ResponseDto? responseDto = await _danamonService.LoginAuth02Async(obj);

            if (responseDto != null && responseDto.IsSuccess)
            {
                var jsonString = JsonConvert.SerializeObject(responseDto.Result);

                authResponseDto = JsonConvert.DeserializeObject<AuthResponseDto>(jsonString);

                //  await SignInUser(authResponseDto);
                // _tokenProvider.SetToken(authResponseDto.access_token);

                return authResponseDto;
            }
            else
            {

                return authResponseDto;
            }

        }

        // GET api/<BillPayController>
        [HttpPost]
        public async Task<ResponseDto> CreateVA(string tokenId, [FromBody] RegistrationVARequest obj)
        {
            AuthResponseDto tkn = JsonConvert.DeserializeObject<AuthResponseDto>(tokenId);

            ResponseDto? response = new ResponseDto();

            if (!string.IsNullOrEmpty(tkn.access_token))
            {
                RegistrationVARequestDto objDto = new RegistrationVARequestDto();

                objDto.registrationVARequest = obj;

                objDto.BDISignature = "f4e4d374c813fd1689bdb1bf1f51653f";
                objDto.BDIKey = Utility.SD.BDIKey;
                objDto.BDITimestamp = DateTime.Now.AddMinutes(-10).ToString("yyyy-MM-ddTHH:HH:mm:ssZ"); //DateTime.Now.AddHours(-1).ToUniversalTime().ToString("o");
                objDto.Authorization = tkn.access_token;

                response = await _danamonService.RegistrationVAAsync(objDto);
            }
            else
            {
                response.Message = "token failed";
            }

            return response;
        }

        [HttpPost]
        public async Task<ResponseDto> AccountInquiryBalance(string tokenId, [FromBody] AccountInquiryBalanceRequest obj)
        {
            AuthResponseDto tkn = JsonConvert.DeserializeObject<AuthResponseDto>(tokenId);
        

            ResponseDto? response = new ResponseDto();

            if (!string.IsNullOrEmpty(tkn.access_token))
            {
                AccountInquiryBalanceRequestDto objDto = new AccountInquiryBalanceRequestDto();

                HeaderDto objHdr = new HeaderDto
                {
                    Authorization = tkn.access_token,
                    BDI_Key = Utility.SD.BDIKey,
                    BDI_Signature = "f4e4d374c813fd1689bdb1bf1f51653f",
                    BDI_Timestamp = DateTime.Now.AddMinutes(-10).ToString("yyyy-MM-ddTHH:HH:mm:ssZ"),
                };

                objDto.AccountInquiryBalanceRequest = obj;
                objDto.Header = objHdr;                

                response = await _danamonService.AccountInquiryBalanceAsync(objDto);
            }
            else
            {
                response.Message = "token failed";
            }

            return response;
        }
    }
}