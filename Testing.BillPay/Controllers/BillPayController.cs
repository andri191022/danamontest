using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Testing.BillPay.Models.Dto;
using Testing.BillPay.Service.IService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Testing.BillPay.Controllers
{
    [Route("api/[controller]")]
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


        // GET: api/<BillPayController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BillPayController>/5
        [HttpGet("{id}")]
        public async Task<string> GetAsync(int id)
        {
            DanamonAuthDto obj = new DanamonAuthDto();

            ResponseDto? responseDto = await _danamonService.LoginAuthAsync(obj);

            if (responseDto != null && responseDto.IsSuccess)
            {
                var jsonString = JsonConvert.SerializeObject(responseDto.Result);

                AuthResponseDto authResponseDto = JsonConvert.DeserializeObject<AuthResponseDto>(jsonString);

                //  await SignInUser(authResponseDto);
               // _tokenProvider.SetToken(authResponseDto.access_token);

                return authResponseDto.access_token;
            }
            else
            {

                return "value";
            }


          
        }

        // POST api/<BillPayController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BillPayController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BillPayController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
