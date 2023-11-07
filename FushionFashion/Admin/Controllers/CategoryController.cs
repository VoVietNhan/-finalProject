using BusinessObject.Dtos.Size;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System;
using BusinessObject.Dtos.Category;
using System.Net.Http.Headers;
using BusinessObject.Dtos.Account;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessObject.Entities.Product;
using System.Text;

namespace Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient client;
        private readonly string CategoryApiUrl;

        public CategoryController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CategoryApiUrl = "https://localhost:44321/api/Category";
        }
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(CategoryApiUrl + "/GetAllcategory");
            string strData = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var cate = JsonSerializer.Deserialize<List<CategoryViewModel>>(strData, option);
            return View(cate);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            HttpResponseMessage response = await client.GetAsync($"{CategoryApiUrl + "/GetProductById"}/{id}");
            string strData = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var cate = JsonSerializer.Deserialize<CategoryViewModel>(strData, option);
            return View(cate);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryViewModel createCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(createCategoryViewModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(CategoryApiUrl + "/CreateCategory", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(createCategoryViewModel);
        }

    }
}
