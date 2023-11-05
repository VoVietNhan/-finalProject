using BusinessObject.Dtos.Account;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly HttpClient client;
        private readonly string AuthenticationApiUrl;

        public AuthenticationController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            AuthenticationApiUrl = "https://localhost:5001/api/Authentication";
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
		[HttpPost]
        public async Task<IActionResult> Login(LoginDtos loginDtos)
        {
            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(loginDtos);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(AuthenticationApiUrl + "/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Shop");
                }
            }
            return View();
        }
    }
}
