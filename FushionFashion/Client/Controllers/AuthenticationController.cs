using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using BusinessObject.Dtos.Account;
using BusinessObject.Entities.Account;
using BusinessObject.Entities.Product;
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

        public async Task<IActionResult> Profile()
        {
            var email = HttpContext.Session.GetString("UserEmail");
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
					return RedirectToAction("Login", "Authentication");
				}
			}
			return View();
		}

		public IActionResult Login()
        {
            return View();
        }
        public IActionResult ChangePassword() {
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
                HttpContext.Session.SetString("UserEmail", loginDtos.Email);
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
