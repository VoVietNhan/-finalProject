using BusinessObject.Dtos.Account;
using BusinessObject.Dtos.Order;
using BusinessObject.Dtos.Product;
using BusinessObject.Entities;
using BusinessObject.Entities.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public ProductController()
        {
            _client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(contentType);
            productUri = "https://localhost:44321/api/Product";
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var productJson = JsonConvert.SerializeObject(model);
                var content = new StringContent(productJson, Encoding.UTF8, "application/json");


                HttpResponseMessage response = await _client.PostAsync(productUri + "/CreateProduct", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Có lỗi khi tạo sản phẩm. Vui lòng thử lại.");
                }
            }
            return View();
        }
    }
}
