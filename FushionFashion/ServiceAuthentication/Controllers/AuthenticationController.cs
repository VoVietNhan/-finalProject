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
        [HttpPost]
        public async Task<ActionResult> Register(RegisterDtos registerDtos)
        {
            var user = await _authenticationService.RegisterUser(registerDtos);
            if (user == null)
            {
                return NotFound();
            }
            return Ok("Register User sucess!");
        }

        /*[HttpPost]
        public Task LogIn(LoginDtos loginDtos)
        {

        }*/
    }
}
