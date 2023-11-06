using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessObject.Dtos.Account;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Client.Controllers
{
	public class UserController : Controller
	{
		private readonly INotyfService _notyfService;
		private readonly HttpClient client;
		private readonly string AuthenticationApiUrl;

		public UserController(INotyfService notyfService)
		{
			_notyfService = notyfService;
			client = new HttpClient();
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			client.DefaultRequestHeaders.Accept.Add(contentType);
			AuthenticationApiUrl = "https://localhost:5001/api/Authentication";
		}

		public IActionResult UpdateUser()
		{
			return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(UpdateUserDtos updateUserDtos)
        {
            var email = HttpContext.Session.GetString("Email");
            var json = JsonSerializer.Serialize(updateUserDtos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"{AuthenticationApiUrl + "/UpdateUser"}/{email}", content);
            if (!response.IsSuccessStatusCode)
            {
                _notyfService.Error("Input is invalid!");
                return View();
            }
            _notyfService.Success("Update information is success!");
            return RedirectToAction("Profile", "Authentication");
        }

        public IActionResult ResetPassword()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
		{
			if (ModelState.IsValid)
			{
				var json = JsonSerializer.Serialize(resetPassword);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await client.PostAsync(AuthenticationApiUrl + "/ResetPasswordUser", content);

				if (response.IsSuccessStatusCode)
				{
					_notyfService.Success("Reset password successfully!");
					return RedirectToAction("Login", "Authentication");

				}
			}
			_notyfService.Error("Input is invalid!");
			return View();
		}
	}
}
