using NuGet.Packaging.Signing;
using Testing.DanamonNew.Models;
using Testing.DanamonNew.Service.IService;
using Testing.DanamonNew.Utility;

namespace Testing.DanamonNew.Service
{
    public class DanamonService : IDanamonService
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
                    BDI_Signature= registvarequestDto.BDISignature,
                    BDI_Key = registvarequestDto.BDIKey,
                    BDI_Timestamp = registvarequestDto.BDITimestamp

                },
                Url = SD.RegisterVaAPIBase,
                ContentType =SD.ContentType.MultipartFormData
            });
        }

        public async Task<ResponseDto?> LoginAuthAsync(DanamonAuthDto loginRequestDto)
        {
            return await _baseService.postAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = SD.AuthAPIBase + "?grant_type=client_credentials"
            });
        }
    }
}
