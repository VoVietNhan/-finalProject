using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using BusinessObject.Dtos.Account;
using BusinessObject.Entities.Account;
using BusinessObject.Entities.Product;
using BusinessObject.Enum.EnumRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly HttpClient client;
        private readonly string AuthenticationApiUrl;
        private readonly string CartApiUrl;

        public AuthenticationController(INotyfService notyfService)
        {
            _notyfService = notyfService;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            AuthenticationApiUrl = "https://localhost:5001/api/Authentication";
            CartApiUrl = "";
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPassword forgotPassword)
        {
            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(forgotPassword);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(AuthenticationApiUrl + "/ForgotPasswordUser", content);

                if (response.IsSuccessStatusCode)
                {
                    _notyfService.Success("Send email successfully!");
                    return View();

                }
            }
            _notyfService.Error("Email is incorrect!");
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
        {
            var email = HttpContext.Session.GetString("Email");
            var json = JsonSerializer.Serialize(changePassword);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync($"{AuthenticationApiUrl + "/ChangePassword"}/{email}", content);
            if (!response.IsSuccessStatusCode)
            {
                _notyfService.Error("OldPassword or NewPassword is invalid!");
                return View();
            }
            _notyfService.Success("Change password is success!");
            return RedirectToAction("Profile", "Authentication");
        }

        public async Task<IActionResult> Profile()
        {
            var email = HttpContext.Session.GetString("Email");
            HttpResponseMessage response = await client.GetAsync($"{AuthenticationApiUrl + "/GetUserByEmail"}/{email}");
            string strData = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var user = JsonSerializer.Deserialize<UserDtos>(strData, option);
            return View(user);
        }

        public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDtos registerDtos)
		{
			if (ModelState.IsValid)
			{
				var json = JsonSerializer.Serialize(registerDtos);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await client.PostAsync(AuthenticationApiUrl + "/Register", content);

				if (response.IsSuccessStatusCode)
				{
                    _notyfService.Success("Register is success!");
					return RedirectToAction("Index", "Shop");
				}
			}
            _notyfService.Error("Input is invalid!");
			return View();
		}

		public IActionResult Login()
        {
            return View();
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDtos loginDtos)
        {
            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(loginDtos);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(AuthenticationApiUrl + "/Login", content);

                string token = await response.Content.ReadAsStringAsync();
                if (token == "{\"status\":\"Error\",\"message\":\"Email or password invalid!\"}")
                {
                    _notyfService.Error("Email or password is invalid!");
                    return RedirectToAction("Index", "Shop");
                }
                HttpContext.Session.SetString("Email", loginDtos.Email);
                HttpContext.Session.SetString("JWT", token);
            }
            _notyfService.Success("Login is success!");
            return RedirectToAction("Index", "Shop");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            //HttpContext.Session.Remove("JWT");
            _notyfService.Success("Logout is success!");
            return RedirectToAction("Index", "Shop");
        }
    }
}
