using BusinessObject.Dtos.Category;
using BusinessObject.Dtos.Product;
using BusinessObject.Dtos.ProductInfo;
using BusinessObject.Dtos.Size;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;

namespace Client.Controllers
{
    public class ShopController : Controller
    {
        Uri cateUri = new Uri("https://localhost:44321/api/Category");
        Uri productUri = new Uri("https://localhost:44321/api/Product");
        Uri infoUri = new Uri("https://localhost:44321/api/ProductInfo");
        Uri size = new Uri("https://localhost:44321/api/Size/GetAllSize");
        private readonly HttpClient _client;
        public ShopController(HttpClient client) { 
            _client = client;
        }

        public IActionResult Index()
        {
            List<CategoryViewModel>  cateList = new List<CategoryViewModel>();
            List<ProductViewModel> productList = new List<ProductViewModel>();
            HttpResponseMessage cateRes = _client.GetAsync(cateUri + "/GetAllcategory").Result;

            HttpResponseMessage respone = _client.GetAsync(productUri+ "/GetAllProduct").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                productList = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);  

            }
            if (cateRes.IsSuccessStatusCode)
            {
                string catedata = cateRes.Content.ReadAsStringAsync().Result;
                cateList = JsonConvert.DeserializeObject<List<CategoryViewModel>>(catedata);
            }
           
            ViewBag.cate = cateList;
            ViewBag.product = productList;
            return View();
        }
        
        public IActionResult Details([FromQuery]Guid productId)
        {
            var product = new ProductViewModel();
            var infoProduct = new List<ProductInfoViewModel>();
            HttpResponseMessage respone = _client.GetAsync(productUri + "/GetProductById" + $"/{productId}").Result;
            HttpResponseMessage infoRespone = _client.GetAsync(infoUri+ "/GetProductInfoByProduct" + $"/{productId}").Result; 
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<ProductViewModel>(data);
            }
            if(infoRespone.IsSuccessStatusCode)
            {
                string infoData = infoRespone.Content.ReadAsStringAsync().Result;
				infoProduct = JsonConvert.DeserializeObject<List<ProductInfoViewModel>>(infoData);
            }
            ViewBag.info = infoProduct;
            return View(product);
        }
        [HttpPost]
        public IActionResult Details()
        {
            return View();
        }
        
    }
}
