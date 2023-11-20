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

namespace Testing.DanamonNew.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public HomeController(ILogger<HomeController> logger, IAuthService authService, ITokenProvider tokenProvider)
        {
            _logger = logger;
            _authService = authService;
            _tokenProvider = tokenProvider;
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

            ResponseDto? responseDto = await _authService.LoginAuthAsync(obj);

            if (responseDto != null && responseDto.IsSuccess)
            {
                var jsonString = JsonConvert.SerializeObject(responseDto.Result);

                AuthResponseDto authResponseDto = JsonConvert.DeserializeObject<AuthResponseDto>(jsonString);

              //  await SignInUser(authResponseDto);
                _tokenProvider.SetToken(authResponseDto.Token);
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

            var jwt = handler.ReadJwtToken(model.Token);

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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}