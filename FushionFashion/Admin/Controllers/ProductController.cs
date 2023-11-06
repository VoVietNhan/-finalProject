using BusinessObject.Dtos.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Admin.Controllers
{
    public class ProductController : Controller
    {
        Uri productUri = new Uri("https://localhost:44321/api/Product");
        private readonly HttpClient _client;
        public ProductController(HttpClient client)
        {
            _client = client;
        }
        public IActionResult Index()
        {
            List<ProductViewModel> productList = new List<ProductViewModel>();

            HttpResponseMessage respone = _client.GetAsync(productUri + "/GetAllProduct").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                productList = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);

            }
            ViewBag.product = productList;
            return View();
        }
    }
}
