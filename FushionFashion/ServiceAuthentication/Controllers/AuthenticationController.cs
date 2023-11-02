using BusinessObject.Dtos.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAuthentication.Services;
using System.Threading.Tasks;

namespace ServiceAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDtos registerDtos)
        {
            var user = await _authenticationService.RegisterUser(registerDtos);
            if (user == null)
            {
                return NotFound();
            }
            return Ok("Register sucess!");
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginUser(LoginDtos loginDtos)
        {
            var user = await _authenticationService.LoginUser(loginDtos);
            if (user != null)
            {
                return Ok("Login Success!");
            }
            return BadRequest("Login Fail!");
        }
    }
}
