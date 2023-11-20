using static Testing.DanamonNew.Utility.SD;
using System.Net;
using System.Text;
using Testing.DanamonNew.Models;
using Testing.DanamonNew.Service.IService;
using Newtonsoft.Json;
using System.Runtime.Intrinsics.Arm;
using System.Security.Policy;
using RestSharp;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Testing.DanamonNew.Service
{

    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto?> postAsync(RequestDto requestDto)
        {
            try
            {
                ResponseDto responseDto = new ResponseDto();
                var options = new RestClientOptions()
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(requestDto.Url, RestSharp.Method.Post);

                //requestDto.Data = null;
                var token = "YjQyMzg2ZTItNGE4ZS00Y2Y2LTkyNjYtNTMzYzk1YWFlMDAyOmU3MGE1NGYyLWEwOGUtNGU1Ni1iZDg3LWIwMzI4ZTEzNTMwMQ==";
                request.AddHeader("Authorization", $"Basic {token}");

                RestResponse response = await client.ExecuteAsync(request);


                if (response.Cookies != null)
                {
                    //string keyBearer = response.Cookies[0].Value;
                    //string nameBearer = response.Cookies[0].Name;

                    responseDto.Message = "";
                    responseDto.IsSuccess = true;
                    responseDto.Result =  new
                    {
                        Token = response.Cookies[0].Value,
                        nameBearer = response.Cookies[0].Name,
                        timeBearer = response.Cookies[0].TimeStamp,
                        expiresBearer = response.Cookies[0].Expires
                    };
                }

                return responseDto;

            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }


        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true, bool oAuth = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                if (requestDto.ContentType == Utility.SD.ContentType.MultipartFormData)
                {
                    message.Headers.Add("Accept", "*/*");
                }
                else
                {
                    message.Headers.Add("Accept", "application/json");
                }
                //token
                if (withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(requestDto.Url);

                if (requestDto.ContentType == Utility.SD.ContentType.MultipartFormData)
                {
                    var content = new MultipartFormDataContent();

                    foreach (var prop in requestDto.Data.GetType().GetProperties())
                    {
                        var value = prop.GetValue(requestDto.Data);
                        if (value is FormFile)
                        {
                            var file = (FormFile)value;
                            if (file != null)
                            {
                                content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                            }
                        }
                        else
                        {
                            content.Add(new StringContent(value == null ? "" : value.ToString()), prop.Name);
                        }
                    }
                    message.Content = content;
                }
                else
                {
                    if (requestDto.Data != null)
                    {
                        message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                    }
                }

                HttpResponseMessage? apiResponse = null;

                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }
        }
    }


}
