using BusinessObject.Dtos.Order;
using BusinessObject.Dtos.Product;
using BusinessObject.Entities;
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
                string products = respone.Content.ReadAsStringAsync().Result;
                productList = JsonConvert.DeserializeObject<List<ProductViewModel>>(products);
            }
            ViewBag.product = productList;
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            
            return View(product);
        }

    }
}
