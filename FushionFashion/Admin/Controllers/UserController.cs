using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using BusinessObject.Dtos.Account;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BusinessObject.Entities.Account;
using BusinessObject.Entities.Product;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Admin.Controllers
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
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(AuthenticationApiUrl + "/GetAllUsers");
            string strData = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var user = JsonSerializer.Deserialize<List<UserDtos>>(strData, option);
            return View(user);
        }

        public async Task<IActionResult> Details(string email)
        {
            HttpResponseMessage response = await client.GetAsync($"{AuthenticationApiUrl + "/GetUserByEmail"}/{email}");
            string strData = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var user = JsonSerializer.Deserialize<UserDtos>(strData, option);
            return View(user);
        }

        public async Task<IActionResult> Edit(string email)
        {
            HttpResponseMessage response = await client.GetAsync($"{AuthenticationApiUrl + "/UpdateStatus"}/{email}");
            _notyfService.Success("Update status user successfully!");
            return RedirectToAction("Index", "User");
        }
    }
}
