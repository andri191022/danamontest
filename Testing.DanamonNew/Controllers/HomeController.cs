using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
using System.Security.Claims;
using Testing.DanamonNew.Models;
using Testing.DanamonNew.Service.IService;
using static System.Net.Mime.MediaTypeNames;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;

namespace Testing.DanamonNew.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        private readonly IDanamonService _danamonService;

        public HomeController(ILogger<HomeController> logger, IAuthService authService, ITokenProvider tokenProvider, IDanamonService danamonService)
        {
            _logger = logger;
            _authService = authService;
            _tokenProvider = tokenProvider;
            _danamonService = danamonService;
        }

        public IActionResult Index()
        {
            string myLabelText = _tokenProvider.GetToken();

            return View((object)myLabelText);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAuthorize()
        {
            DanamonAuthDto obj = new();
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> GetAuthorize(DanamonAuthDto obj)
        {

            ResponseDto? responseDto = await _danamonService.LoginAuthAsync(obj);

            if (responseDto != null && responseDto.IsSuccess)
            {
                var jsonString = JsonConvert.SerializeObject(responseDto.Result);

                AuthResponseDto authResponseDto = JsonConvert.DeserializeObject<AuthResponseDto>(jsonString);

                //  await SignInUser(authResponseDto);
                _tokenProvider.SetToken(authResponseDto.access_token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = responseDto.Message;
                return View(obj);
            }


        }

        private async Task SignInUser(AuthResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(model.access_token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            //identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
            //    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            //identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
            //    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));


            //identity.AddClaim(new Claim(ClaimTypes.Name,
            //    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            //identity.AddClaim(new Claim(ClaimTypes.Role,
            //    jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));



            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }


        [HttpGet]
        public IActionResult CreateVA()
        {
            Guid guid = Guid.NewGuid();
            Random random = new Random();
            int i = random.Next();

            RegistrationVARequest obj = new RegistrationVARequest();
            obj.UserReferenceNumber = (i.ToString() + i.ToString()).Substring(0, 16); //"1200123456784888";
            obj.VirtualAccountNumber = "8888000000654321";
            obj.VirtualAccountName = "MEDIO MAYO";
            obj.VirtualAccountExpiryDate = DateTime.Now.AddDays(5).ToString("yyyyMMddhhmmss");
            obj.RequestTime = DateTime.Now.ToString("yyyyMMddhhmmss");


            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVA(RegistrationVARequest obj)
        {

            if (!string.IsNullOrEmpty(_tokenProvider.GetToken()))
            {
                RegistrationVARequestDto objDto = new RegistrationVARequestDto();

                objDto.registrationVARequest = obj;

                objDto.BDISignature = "f4e4d374c813fd1689bdb1bf1f51653f";
                objDto.BDIKey = Utility.SD.BDIKey;
                objDto.BDITimestamp = DateTime.Now.ToUniversalTime().ToString("o");
                

                ResponseDto? response = await _danamonService.RegistrationVAAsync(objDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = response?.Message;
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            else
            {
                TempData["error"] = "Token belum ada, Get Token dahulu";
            }


            return View(obj);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}