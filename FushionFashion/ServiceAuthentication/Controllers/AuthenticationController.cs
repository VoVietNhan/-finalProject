using AutoMapper;
using BusinessObject.Dtos.Account;
using BusinessObject.Entities.Account;
using BusinessObject.Enum.EnumRole;
using Common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAuthentication.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenticationController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, IEmailService emailService, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
            _mapper = mapper;
        }
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<UserDtos>>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users == null || users.Count < 0)
            {
                return NotFound("List user empty!");
            }
            return _mapper.Map<List<UserDtos>>(users);
        }

        [HttpGet("GetUserByEmail/{email}")]
        public async Task<ActionResult<UserDtos>> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User doesn't exists!");
            }
            return _mapper.Map<UserDtos>(user);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterDtos registerDtos)
        {
            var userExits = await _userManager.FindByEmailAsync(registerDtos.Email);
            if (userExits != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "Error", Message = "User already exists!" });
            }

            AppUser user = new()
            {
                UserName = registerDtos.Email,
                Email = registerDtos.Email,
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
                var message = new Message(new string[] { user.Email! }, "Confirmation Email", "<p style = \"text-align: center; color: black; font-weight: bold; font-size: 2em;\">This is email confirm!</p>" +
                    $"<div style=\"text-align: center;\"><a style=\"text-decoration: none; background-color: #0074e4; color: white; padding: 10px 20px; border-radius: 5px; font-weight: bold; font-size: 1.2em;\" href=\"{confirmationLink!}\">Click me</a></div>");
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

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginDtos loginDtos)
        {
            var user = await _userManager.FindByEmailAsync(loginDtos.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDtos.Password))
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    return Unauthorized("Please confirm your email before logging in!");
                }
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {                 
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Id", user.Id.ToString()),
					new Claim("Fullname", user.Fullname),
					new Claim("Email", user.Email),
					new Claim("PhoneNumber", user.PhoneNumber),
					new Claim("Address", user.Address),
					new Claim("Status", user.Status.ToString())
				};
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                var jwtToken = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
					expiration = jwtToken.ValidTo
				});
            }
            return StatusCode(StatusCodes.Status401Unauthorized, new Response { Status = "Error", Message = "Email or password invalid!" });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
            return token;
        }

        [HttpPost("ChangePassword/{email}")]
        public async Task<IActionResult> ChangePasswordUser(string email, ChangePassword changePassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "User doesn't exists!" });
            }
            var change = await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
            if (!change.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Failed to change password!" });
            }
            await _signInManager.RefreshSignInAsync(user);
            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "User change password successfully!" });
        }

        [HttpPost("ForgotPasswordUser")]
        public async Task<IActionResult> ForgotPasswordUser(ForgotPassword forgotPassword)
        {
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var link = Url.Action(nameof(ResetPassword), "Authentication", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Forgot Password", "<p style = \"text-align: center; color: black; font-weight: bold; font-size: 2em;\">This is email reset password!</p>" +
                    $"<div style=\"text-align: center;\"><a text-decoration: none; background-color: #0074e4; color: white; padding: 10px 20px; border-radius: 5px; font-weight: bold; font-size: 1.2em;\" href=\"{link!}\">Click me</a></div>");
                _emailService.SendEmail(message);
                return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Password changed request sent to email!" });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Couldn't send link to email!" });
        }

        [HttpGet("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var model = new ResetPassword
                {
                    Email = email,
                    Token = token
                };
                return Ok(new
                {
                    model
                });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Undefined error!" });
        }

        [HttpPost("ResetPasswordUser")]
        public async Task<IActionResult> ResetPasswordUser(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user != null)
            {
                var reset = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
                if (!reset.Succeeded)
                {
                    foreach (var error in reset.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }
                return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Password have been changed!" });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Success", Message = "Undefined error!" });
        }
    }
}