using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using System.Text;
using Testing.BillPay.Logic;
using Testing.BillPay.Models.Dto;
using Testing.BillPay.Service.IService;
using static Testing.BillPay.Utility.SD;

namespace Testing.BillPay.Service
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
                    RemoteCertificateValidationCallback = (sender, certification, chain, sslPolicyError) => true,
                    //  Proxy= new System.Net.WebProxy("http://idnproxy",8080),  //----> jika menggunakan proxy
                };
                var client = new RestClient(options);
                var request = new RestRequest(requestDto.Url, RestSharp.Method.Post);

                //requestDto.Data = null;
                var encdo = System.Text.Encoding.UTF8.GetBytes(Utility.SD.OACClientID + ":" + Utility.SD.OACClientIDSecret);

                string token = System.Convert.ToBase64String(encdo);
                request.AddHeader("Authorization", $"Basic {token}");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("grant_type", "client_credentials");

                RestResponse response = await client.ExecuteAsync(request);

                switch (response.StatusCode)
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
                        AuthResponseDto apiContent = JsonConvert.DeserializeObject<AuthResponseDto>(response.Content);  //  JsonConvert.SerializeObject(response.Content);
                        ResponseDto apiResponseDto = new ResponseDto();
                        apiResponseDto.IsSuccess = true;
                        apiResponseDto.Message = "";
                        apiResponseDto.Result = apiContent;

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

        public async Task<ResponseDto?> postB2BAsync(RequestDto requestDto)
        {
            try
            {
                ResponseDto responseDto = new ResponseDto();
                var options = new RestClientOptions()
                {
                    MaxTimeout = -1,
                    RemoteCertificateValidationCallback = (sender, certification, chain, sslPolicyError) => true,
                    //  Proxy= new System.Net.WebProxy("http://idnproxy",8080),  //----> jika menggunakan proxy
                };
                var client = new RestClient(options);
                var request = new RestRequest(requestDto.Url, RestSharp.Method.Post);

                //requestDto.Data = null;
                //var encdo = System.Text.Encoding.UTF8.GetBytes(Utility.SD.OACClientID + ":" + Utility.SD.OACClientIDSecret);

                // string token = System.Convert.ToBase64String(encdo);
                //request.AddHeader("Authorization", $"Basic {token}");
                string dtTime = DateTime.Now.ToString("yyyy-MM-ddThh:mm:sszzz");
                request.AddHeader("X-TIMESTAMP", dtTime);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-CLIENT-KEY", Utility.SD.OACClientID);
                request.AddHeader("X-SIGNITURE", GeneralLogic.GenerateSigniture(Utility.SD.ApiType.POST, Utility.SD.FunctionDBIType.SC_73, Utility.SD.OACClientID + "|" + dtTime));

                request.AddParameter("grantType", "client_credentials");

                RestResponse response = await client.ExecuteAsync(request);

                switch (response.StatusCode)
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
                        string filePath = Path.Combine(Environment.CurrentDirectory, @"Data\", "log.txt");
                        File.AppendAllText(filePath, response.Content + Environment.NewLine);

                        AuthResponseDto apiContent = JsonConvert.DeserializeObject<AuthResponseDto>(response.Content);  //  JsonConvert.SerializeObject(response.Content);
                        ResponseDto apiResponseDto = new ResponseDto();
                        apiResponseDto.IsSuccess = true;
                        apiResponseDto.Message = "";
                        apiResponseDto.Result = apiContent;

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


        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");

                HttpClientHandler httpHandler = new HttpClientHandler
                {
                    Proxy = new System.Net.WebProxy("http://idnproxy", 8080),
                };

                // client = new HttpClient(httpHandler); //----> jika menggunakan proxy

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
                //if (withBearer)
                //{
                //    var token = ""; //_tokenProvider.GetToken();
                //    message.Headers.Add("Authorization", $"Bearer {token}");
                //}
                //header
                if (requestDto.Header != null)
                {
                    string hdr = JsonConvert.SerializeObject(requestDto.Header);
                    HeaderDto headerDto = JsonConvert.DeserializeObject<HeaderDto>(hdr);
                    if (!string.IsNullOrEmpty(headerDto.BDI_Key)) message.Headers.Add("BDI-Key", headerDto.BDI_Key);
                    if (!string.IsNullOrEmpty(headerDto.BDI_Timestamp)) message.Headers.Add("BDI-Timestamp", headerDto.BDI_Timestamp);
                    if (!string.IsNullOrEmpty(headerDto.BDI_Signature)) message.Headers.Add("BDI-Signature", headerDto.BDI_Signature);
                    if (!string.IsNullOrEmpty(headerDto.Authorization)) message.Headers.Add("Authorization", headerDto.Authorization);

                }
                //

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
                        ResponseDto apiResponseDto = new ResponseDto();
                        if (apiContent.Contains("Request Rejected"))
                        {
                            apiResponseDto.IsSuccess = true;
                            apiResponseDto.Message = apiContent;
                        }
                        else
                        {
                            apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        }

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
