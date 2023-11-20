using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Nest;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
using Testing.DanamonNew.Models;
using Testing.DanamonNew.Service.IService;
using static System.Net.Mime.MediaTypeNames;

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
            return View();
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

            return View(obj);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}