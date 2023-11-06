using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Collections.Generic;
using BusinessObject.Dtos.Category;
using BusinessObject.Dtos.Product;
using Newtonsoft.Json;
using BusinessObject.Dtos.Size;
using BusinessObject.Dtos.Order;
using BusinessObject.Entities.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Admin.Controllers
{
    public class AdminController : Controller
    {
        //Uri cateUri = new Uri("https://localhost:44321/api/Category");
        //Uri productUri = new Uri("https://localhost:44321/api/Product");
        //Uri infoUri = new Uri("https://localhost:44321/api/ProductInfo");
        //Uri orderUri = new Uri("https://localhost:44321/api/Order");
        //Uri size = new Uri("https://localhost:44321/api/Size/GetAllSize");
        //private readonly HttpClient _client;
        //public AdminController(HttpClient client)
        //{
        //    _client = client;
        //}
        //public IActionResult Index()
        //{
        //    List<CategoryViewModel> cateList = new List<CategoryViewModel>();
        //    List<ProductViewModel> productList = new List<ProductViewModel>();
        //    List<SizeViewModel> sizeList = new List<SizeViewModel>();
        //    List<OrderDTO> orderList = new List<OrderDTO>();

        //    HttpResponseMessage cateRes = _client.GetAsync(cateUri + "/GetAllcategory").Result;

        //    HttpResponseMessage respone = _client.GetAsync(productUri + "/GetAllProduct").Result;
        //    HttpResponseMessage size = _client.GetAsync(productUri + "/GetAllSize").Result;
        //    HttpResponseMessage order = _client.GetAsync(productUri + "/GetALlOrder").Result;
        //    if (respone.IsSuccessStatusCode)
        //    {
        //        string data = respone.Content.ReadAsStringAsync().Result;
        //        productList = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);

        //    }
        //    if (cateRes.IsSuccessStatusCode)
        //    {
        //        string catedata = cateRes.Content.ReadAsStringAsync().Result;
        //        cateList = JsonConvert.DeserializeObject<List<CategoryViewModel>>(catedata);
        //    }
        //    if (size.IsSuccessStatusCode)
        //    {
        //        string sizes = size.Content.ReadAsStringAsync().Result;
        //        sizeList = JsonConvert.DeserializeObject<List<SizeViewModel>> (sizes);
        //    }
        //    if (order.IsSuccessStatusCode)
        //    {
        //        string orders = order.Content.ReadAsStringAsync().Result;
        //        orderList = JsonConvert.DeserializeObject<List<OrderDTO>>(orders);
        //    }
        //    ViewBag.product = orderList;
        //    ViewBag.size = sizeList;
        //    ViewBag.cate = cateList;
        //    ViewBag.product = productList;
        //    return View();
        //}
    }
}
