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
        public IActionResult Index()
        {
            return View();
        }
    }
}
