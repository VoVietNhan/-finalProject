using BusinessObject.Dtos.Account;
using BusinessObject.Dtos.Category;
using BusinessObject.Dtos.Order;
using BusinessObject.Dtos.Product;
using BusinessObject.Dtos.Size;
using BusinessObject.Entities;
using BusinessObject.Entities.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _client;
        private readonly string productUri;
        private readonly string cateUri;
        private readonly string sizeUri;
        Uri infoUri = new Uri("https://localhost:44321/api/ProductInfo");
        public ProductController()
        {
            _client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(contentType);
            productUri = "https://localhost:44321/api/Product";
            cateUri = "https://localhost:44321/api/Category";
            sizeUri = "https://localhost:44321/api/Size";
        }
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _client.GetAsync(productUri + "/GetAllProduct");
            string strData = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var product = System.Text.Json.JsonSerializer.Deserialize<List<ProductViewModel>>(strData, option);
            return View(product);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            HttpResponseMessage response = await _client.GetAsync(productUri + "/GetProductById/" + id);
            string strData = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var Product = JsonSerializer.Deserialize<ProductViewModel>(strData, option);
            return View(Product);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            HttpResponseMessage response = await _client.GetAsync(productUri + "/GetProductById/" + id);
            string strData = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var Product = JsonSerializer.Deserialize<ProductViewModel>(strData, option);
            return View(Product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel updatedProduct)
        {
            if (id != updatedProduct.Id)
            {
               
                return View(updatedProduct);
            }

            var json = JsonSerializer.Serialize(updatedProduct);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(productUri + "/UpdateProduct", content);

            if (response.IsSuccessStatusCode)
            {
                
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(updatedProduct);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            HttpResponseMessage response = _client.GetAsync(sizeUri + "/GetAllSize").Result;
            string strData = response.Content.ReadAsStringAsync().Result;

            var size = JsonConvert.DeserializeObject<List<SizeViewModel>>(strData);
            HttpResponseMessage cateresponse = _client.GetAsync(cateUri + "/GetAllcategory").Result;
            string cateData = cateresponse.Content.ReadAsStringAsync().Result;

            var cate = JsonConvert.DeserializeObject<List<CategoryViewModel>>(cateData);
            ViewBag.Cate = cate;
            ViewBag.Size = size;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Description = model.Description,
                    Image = model.Image,
                    Price = model.Price,
                    CategoryId = model.CategoryId                    
                };
                var productJson = JsonSerializer.Serialize(product);
                var content = new StringContent(productJson, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(productUri + "/CreateProduct", content);

                var infoProduct = new ProductInfo
                {
                    ProductId = product.Id,
                    SizeId = model.SizeId,
                    Quantity = model.Quantity,
                };
                var infoProductJson = JsonSerializer.Serialize(infoProduct);
                var infocontent = new StringContent(infoProductJson, Encoding.UTF8, "application/json");

                HttpResponseMessage infoResponse = await _client.PostAsync(infoUri + "/CreateProductInfo", infocontent);
            }
            return RedirectToAction("Index","Product");
            }

    }
}
