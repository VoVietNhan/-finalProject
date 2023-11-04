using BusinessObject.Dtos.Category;
using BusinessObject.Dtos.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Client.Controllers
{
    public class ShopController : Controller
    {
        Uri productUri = new Uri("https://localhost:44321/api/Product");
        Uri cateUri = new Uri("https://localhost:44321/api/Category");
        private readonly HttpClient _client;
        public ShopController(HttpClient client) { 
            _client = client;
        }
        public IActionResult Index()
        {
            List<CategoryViewModel>  cateList = new List<CategoryViewModel>();
            List<ProductViewModel> productList = new List<ProductViewModel>();
            HttpResponseMessage respone = _client.GetAsync(productUri+ "/GetAllProduct").Result;
            HttpResponseMessage cateRes = _client.GetAsync(cateUri+ "/GetAllcategory").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                productList = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);  

            }
            if (cateRes.IsSuccessStatusCode)
            {
                string catedata = respone.Content.ReadAsStringAsync().Result;
                cateList = JsonConvert.DeserializeObject<List<CategoryViewModel>>(catedata);
            }
            ViewBag.cate = cateList;
            ViewBag.product = productList;
            return View();
        }
        
    }
}
