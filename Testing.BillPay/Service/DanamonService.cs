using Testing.BillPay.Models.Dto;
using Testing.BillPay.Service.IService;
using Testing.BillPay.Utility;

namespace Testing.BillPay.Service
{
    public class DanamonService: IDanamonService
    {
        private readonly IBaseService _baseService;

        public DanamonService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> RegistrationVAAsync(RegistrationVARequestDto registvarequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registvarequestDto.registrationVARequest,
                Header = new HeaderDto()
                {
                    BDI_Signature = registvarequestDto.BDISignature,
                    BDI_Key = registvarequestDto.BDIKey,
                    BDI_Timestamp = registvarequestDto.BDITimestamp,
                    Authorization= @"Bearer " + registvarequestDto.Authorization

                },
                Url = SD.RegisterVaAPIBase,
                ContentType = SD.ContentType.Json
            }, false);
        }

        public async Task<ResponseDto?> LoginAuthAsync(DanamonAuthDto loginRequestDto)
        {
            return await _baseService.postAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = SD.AuthAPIBase  //+ "?grant_type=client_credentials"
            });
        }

        public async Task<ResponseDto?> AccountInquiryBalanceAsync(AccountInquiryBalanceRequestDto accountInquiryBalanceRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = accountInquiryBalanceRequestDto.AccountInquiryBalanceRequest,
                Header = accountInquiryBalanceRequestDto.Header,
                Url = SD.RegisterVaAPIBase,
                ContentType = SD.ContentType.Json
            });
        }

        public async Task<ResponseDto?> LoginAuth02Async(Auth02Dto auth02Dto)
        {
            return await _baseService.postAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = auth02Dto,
                Url = SD.AuthAPIBase  //+ "?grant_type=client_credentials"
            });
        }
    }
}
