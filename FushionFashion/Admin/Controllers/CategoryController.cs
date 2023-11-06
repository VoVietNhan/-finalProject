using BusinessObject.Dtos.Size;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System;
using BusinessObject.Dtos.Category;

namespace Admin.Controllers
{
    public class CategoryController : Controller
    {
        Uri cateUri = new Uri("https://localhost:44321/api/Category");
        private readonly HttpClient _client;
        public CategoryController(HttpClient client)
        {
            _client = client;
        }
        public IActionResult Index()
        {
            List<CategoryViewModel> cateList = new List<CategoryViewModel>();

            HttpResponseMessage respone = _client.GetAsync(cateUri + "/GetAllCategory").Result;
            if (respone.IsSuccessStatusCode)
            {
                string cate = respone.Content.ReadAsStringAsync().Result;
                cateList = JsonConvert.DeserializeObject<List<CategoryViewModel>>(cate);
            }
            ViewBag.product = cateList;
            return View();
        }


    }
}
