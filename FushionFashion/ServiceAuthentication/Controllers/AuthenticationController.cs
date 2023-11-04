using BusinessObject.Dtos.Account;
using BusinessObject.Entities.Account;
using BusinessObject.Enum.EnumRole;
using Common.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceAuthentication.Services;
using System;
using System.Threading.Tasks;

namespace ServiceAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public AuthenticationController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterDtos registerDtos)
        {
            var userExits = await _userManager.FindByEmailAsync(registerDtos.Username);
            if (userExits != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "Error", Message = "User already exists!" });
            }

            AppUser user = new()
            {
                UserName = registerDtos.Username,
                Email = registerDtos.Username,
                Fullname = registerDtos.Fullname,
                PhoneNumber = registerDtos.Phone,
                Address = registerDtos.Address,
                Status = BusinessObject.Enum.EnumStatus.EnumStatus.Enable,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(user, registerDtos.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User failed to create!" });
            }
            if (await _roleManager.RoleExistsAsync(Role.User.ToString()))
            {
                await _userManager.AddToRoleAsync(user, Role.User.ToString());

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Confirmation email link", "<p style = \"text-align: center; color: black; font-weight: bold; font-size: 2em;\">This is email confirm!</p>" +
                    $"<a style=\"text-align: center; text-decoration: none; background-color: #0074e4; color: white; padding: 10px 20px; border-radius: 5px; font-weight: bold; font-size: 1.2em;\" href=\"{confirmationLink!}\">Click me</a>");
                _emailService.SendEmail(message);
                return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "User created & email sent to successfully!" });
            } 
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User failed to create!" });
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var userExits = await _userManager.FindByEmailAsync(email);
            if (userExits != null)
            {
                var result = await _userManager.ConfirmEmailAsync(userExits, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Email verified successfully!" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User doesn't exists!" });
        }
    }
}
