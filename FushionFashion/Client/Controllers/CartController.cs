using BusinessObject.Dtos.Product;
using BusinessObject.Dtos.ProductInfo;
using BusinessObject.Entities.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Client.Controllers
{
    public class CartController : Controller
    {
        Uri cateUri = new Uri("https://localhost:44321/api/Category");
        Uri productUri = new Uri("https://localhost:44321/api/Product");
        Uri infoUri = new Uri("https://localhost:44321/api/ProductInfo");
        Uri size = new Uri("https://localhost:44321/api/Size/GetAllSize");
        private readonly HttpClient _client;
        public CartController(HttpClient client)
        {
            _client = client;
        }
        [HttpPost]
        public IActionResult AddToCart(List<string> productIds, List<string> sizeIds)

        {
            var list = sizeIds;
            var listProduct = productIds;
            return RedirectToAction("Index");
        }
        public IActionResult Index([FromQuery] Guid productId)
        {
            var product = new ProductViewModel();
            var infoProduct = new List<ProductInfoViewModel>();
            HttpResponseMessage respone = _client.GetAsync(productUri + "/GetProductById" + $"/{productId}").Result;
            HttpResponseMessage infoRespone = _client.GetAsync(infoUri + "/GetProductInfoByProduct" + $"/{productId}").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<ProductViewModel>(data);
            }
            if (infoRespone.IsSuccessStatusCode)
            {
                string infoData = infoRespone.Content.ReadAsStringAsync().Result;
                infoProduct = JsonConvert.DeserializeObject<List<ProductInfoViewModel>>(infoData);
            }
            return View();
        }
    }
}
