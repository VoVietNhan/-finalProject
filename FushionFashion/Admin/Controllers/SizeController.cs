using BusinessObject.Dtos.Order;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System;
using BusinessObject.Dtos.Size;

namespace Admin.Controllers
{
    public class SizeController : Controller
    {
        Uri sizeUri = new Uri("https://localhost:44321/api/Size");
        private readonly HttpClient _client;
        public SizeController(HttpClient client)
        {
            _client = client;
        }
        public IActionResult Index()
        {
            List<SizeViewModel> sizeList = new List<SizeViewModel>();

            HttpResponseMessage respone = _client.GetAsync(sizeUri + "/GetAllSize").Result;
            if (respone.IsSuccessStatusCode)
            {
                string sizes  = respone.Content.ReadAsStringAsync().Result;
                sizeList = JsonConvert.DeserializeObject<List<SizeViewModel>>(sizes);
            }
            ViewBag.product = sizeList;
            return View();
        }
    }
}
