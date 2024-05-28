using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using System.Security.Claims;
using System.Text;
using WorkerService2.Logic;
using WorkerService2.Model;
using WorkerService2.Model.Dto;
using WorkerService2.Service.IService;


namespace WorkerService2
{

    public class CompanyBackgroundService : BackgroundService
    {
        private readonly ICompany _companyRepository;
        private readonly ILogger<Worker> _logger;
        // private readonly IHttpContextAccessor _httpContextAccessor;

        public CompanyBackgroundService(ICompany companyRepository, ILogger<Worker> logger)//, IHttpContextAccessor httpContextAccessor)
        {
            _companyRepository = companyRepository;
            _logger = logger;
            // _httpContextAccessor = httpContextAccessor;
        }

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    // ISession session = _httpContextAccessor.HttpContext.Session;
        //    string access_token = string.Empty;
        //    DateTime expired_token = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta"));
        //    while (!stoppingToken.IsCancellationRequested)
        //    {

        //        //DateTime expires = DateTime.Now;
        //        // string expires = "0";

        //        RequestDto rdto = new RequestDto();
        //        rdto.Url = AuthAPIBase;


        //        ResponseDto? response = new ResponseDto();
        //        if (Int64.Parse(TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta")).ToString("yyyyMMddHHmm")) >= Int64.Parse(expired_token.ToString("yyyyMMddHHmm")))
        //        {
        //            response = await postAsync(rdto);
        //            if (response.IsSuccess)
        //            {
        //                AuthResponseDto? apiContent = response.Result as AuthResponseDto; //JsonConvert.DeserializeObject<AuthResponseDto>(response.Result);

        //                access_token = apiContent.access_token;
        //                expired_token = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta")).AddMinutes((apiContent.expires_in / 60) - 50);

        //            }
        //        }


        //        // _logger.LogInformation($"Worker running at: {JsonConvert.SerializeObject(response.Result)}");

        //        _logger.LogInformation($"Worker running:{access_token},  {expired_token.ToString("yyyyMMdd HHmm")}");
        //        List<tb_SMK> obj = (List<tb_SMK>)await _companyRepository.GetCompanies();
        //        _logger.LogInformation($"total data {obj.Count().ToString()}");

        //        // await Task.Delay(1000, stoppingToken);
        //        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        //    }




        //}

        //public async Task<ResponseDto?> postAsync(RequestDto requestDto)
        //{
        //    try
        //    {
        //        ResponseDto responseDto = new ResponseDto();
        //        var options = new RestClientOptions()
        //        {
        //           //MaxTimeout = -1,
        //            RemoteCertificateValidationCallback = (sender, certification, chain, sslPolicyError) => true,
        //            // Proxy = new System.Net.WebProxy(SD.JktProxy ?? "", SD.JktPort ?? 0),  //----> jika menggunakan proxy
        //        };
        //        var client = new RestClient(options);
        //        var request = new RestRequest(requestDto.Url, RestSharp.Method.Post);

        //        //requestDto.Data = null;
        //        var encdo = System.Text.Encoding.UTF8.GetBytes(Utility.SD.OACClientID + ":" + Utility.SD.OACClientIDSecret);

        //        string token = System.Convert.ToBase64String(encdo);
        //        request.AddHeader("Authorization", $"Basic {token}");
        //        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        //        request.AddParameter("grant_type", "client_credentials");

        //        RestResponse response = await client.ExecuteAsync(request);

        //        switch (response.StatusCode)
        //        {
        //            case HttpStatusCode.NotFound:
        //                return new() { IsSuccess = false, Message = "Not Found" };
        //            case HttpStatusCode.Forbidden:
        //                return new() { IsSuccess = false, Message = "Access Denied" };
        //            case HttpStatusCode.Unauthorized:
        //                return new() { IsSuccess = false, Message = "Unauthorized" };
        //            case HttpStatusCode.InternalServerError:
        //                return new() { IsSuccess = false, Message = "Internal Server Error" };
        //            default:
        //                AuthResponseDto apiContent = JsonConvert.DeserializeObject<AuthResponseDto>(response.Content);  //  JsonConvert.SerializeObject(response.Content);
        //                ResponseDto apiResponseDto = new ResponseDto();
        //                apiResponseDto.IsSuccess = true;
        //                apiResponseDto.Message = "";
        //                apiResponseDto.Result = apiContent;

        //                return apiResponseDto;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var dto = new ResponseDto
        //        {
        //            Message = ex.Message.ToString(),
        //            IsSuccess = false
        //        };
        //        return dto;
        //    }


        //}


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // ISession session = _httpContextAccessor.HttpContext.Session;
            string access_token = string.Empty;
            DateTime expired_token = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta"));
            while (!stoppingToken.IsCancellationRequested)
            {

                //DateTime expires = DateTime.Now;
                // string expires = "0";

                RequestDto rdto = new RequestDto();
                rdto.Url = Utility.SD.AuthB2BAPIBase;


                ResponseDto? response = new ResponseDto();
                if (Int64.Parse(TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta")).ToString("yyyyMMddHHmm")) >= Int64.Parse(expired_token.ToString("yyyyMMddHHmm")))
                {
                    response = await GetTokenB2B(rdto);
                    if (response.IsSuccess)
                    {
                        AuthResponseDto? apiContent = response.Result as AuthResponseDto; //JsonConvert.DeserializeObject<AuthResponseDto>(response.Result);

                        access_token = apiContent.access_token;
                        expired_token = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta")).AddMinutes((apiContent.expires_in / 60) - 50);

                    }

                    //response = await postAsync(rdto);
                    //if (response.IsSuccess)
                    //{
                    //    AuthResponseDto? apiContent = response.Result as AuthResponseDto; //JsonConvert.DeserializeObject<AuthResponseDto>(response.Result);

                    //    access_token = apiContent.access_token;
                    //    expired_token = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta")).AddMinutes((apiContent.expires_in / 60) - 50);

                    //}
                }


                // _logger.LogInformation($"Worker running at: {JsonConvert.SerializeObject(response.Result)}");

                _logger.LogInformation($"Worker running:{access_token},  {expired_token.ToString("yyyyMMdd HHmm")}");
                //List<tb_SMK> obj = (List<tb_SMK>)await _companyRepository.GetCompanies();
                //_logger.LogInformation($"total data {obj.Count().ToString()}");

                // await Task.Delay(1000, stoppingToken);
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }




        }

        public async Task<ResponseDto?> postAsync(RequestDto requestDto)
        {
            try
            {
                ResponseDto responseDto = new ResponseDto();
                var options = new RestClientOptions()
                {
                    //MaxTimeout = -1,
                    RemoteCertificateValidationCallback = (sender, certification, chain, sslPolicyError) => true,
                    // Proxy = new System.Net.WebProxy(SD.JktProxy ?? "", SD.JktPort ?? 0),  //----> jika menggunakan proxy
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

        public async Task<ResponseDto?> GetTokenB2B(RequestDto requestDto)
        {
            try
            {
                ResponseDto responseDto = new ResponseDto();
                var options = new RestClientOptions()
                {
                   // MaxTimeout = -1,
                    RemoteCertificateValidationCallback = (sender, certification, chain, sslPolicyError) => true,
                    Proxy = new System.Net.WebProxy("http://idnproxy", 8080),  //----> jika menggunakan proxy
                };
                var client = new RestClient(options);
                var request = new RestRequest(requestDto.Url, RestSharp.Method.Post);

                //requestDto.Data = null;
                //var encdo = System.Text.Encoding.UTF8.GetBytes(Utility.SD.OACClientID + ":" + Utility.SD.OACClientIDSecret);

                // string token = System.Convert.ToBase64String(encdo);
                //request.AddHeader("Authorization", $"Basic {token}");

                string dtTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
                string signiturex = GeneralLogic.GenerateSigniture(Utility.SD.ApiType.POST, Utility.SD.FunctionDBIType.SC_73, Utility.SD.OACClientID + "|" + dtTime);


                Console.WriteLine(dtTime);
                Console.WriteLine(Utility.SD.OACClientID);
                Console.WriteLine(signiturex);

                request.AddHeader("X-TIMESTAMP", dtTime);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-CLIENT-KEY", Utility.SD.OACClientID);
                request.AddHeader("X-SIGNATURE", signiturex);

                var reqBody = new { grantType = "client_credentials" };
                var jsonBody = JsonConvert.SerializeObject(reqBody);
                request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);


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


    }
}
