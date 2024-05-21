using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Testing.BillPay.Models;
using Testing.BillPay.Models.Dto;
using Testing.BillPay.Service.IService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Testing.BillPay.Controllers
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

        // POST api/<BillPayController>
        [HttpPost]
        public async Task<AuthResponseDto> GetAuth02([FromBody] Auth02Dto obj)
        {
            //DanamonAuthDto obj = new DanamonAuthDto();
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


        [HttpPost]
        public async Task<AuthResponseB2BDto> GetAuthB2B()
        {
            //DanamonAuthDto obj = new DanamonAuthDto();
            AuthResponseB2BDto authResponseDto = new AuthResponseB2BDto();

            ResponseDto? responseDto = await _danamonService.LoginAuthB2BAsync();

            if (responseDto != null && responseDto.IsSuccess)
            {
                var jsonString = JsonConvert.SerializeObject(responseDto.Result);

                authResponseDto = JsonConvert.DeserializeObject<AuthResponseB2BDto>(jsonString);

                //  await SignInUser(authResponseDto);
                // _tokenProvider.SetToken(authResponseDto.access_token);

                return authResponseDto;
            }
            else
            {

                return authResponseDto;
            }

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
