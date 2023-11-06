using BusinessObject.Dtos.Account;
using BusinessObject.Dtos.CartDetail;
using BusinessObject.Dtos.Product;
using BusinessObject.Dtos.ProductInfo;
using BusinessObject.Entities.Cart;
using BusinessObject.Entities.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Client.Controllers
{
    public class CartController : Controller
    {
        Uri productUri = new Uri("https://localhost:44321/api/Product");
        Uri infoUri = new Uri("https://localhost:44321/api/ProductInfo");
        Uri authentication = new Uri("https://localhost:5001/api/Authentication");
        Uri cartUri = new Uri("https://localhost:44340/api/Cart");
        Uri detailURi = new Uri("https://localhost:44340/api/CartDetail/CreateCartDetai");
        private readonly HttpClient _client;
        public CartController(HttpClient client)
        {
            _client = client;
        }
        [HttpPost]
        public IActionResult AddToCart(Guid productInfoId)
        {
            var infoProduct = new ProductInfoViewModel();
            HttpResponseMessage infoRespone = _client.GetAsync(infoUri + "/GetProductInfoById" + $"/{productInfoId}").Result;
            if (infoRespone.IsSuccessStatusCode)
            {
                string infoData = infoRespone.Content.ReadAsStringAsync().Result;
                infoProduct = JsonConvert.DeserializeObject<ProductInfoViewModel>(infoData);
            }
            string mail = HttpContext.Session.GetString("Email");
            if (mail != null)
            {
                HttpResponseMessage respone = _client.GetAsync(authentication + "/GetUserByEmail" + $"/{mail}").Result;
                if (respone.IsSuccessStatusCode)
                {
                    string data = respone.Content.ReadAsStringAsync().Result;
                    var user = JsonConvert.DeserializeObject<UserDtos>(data);
                    if (user != null)
                    {
                        HttpResponseMessage cartRespone = _client.GetAsync(cartUri + $"?Userid={user.Id}").Result;
                        if (cartRespone.IsSuccessStatusCode)
                        {

                            string cartData = cartRespone.Content.ReadAsStringAsync().Result;
                            var cart = JsonConvert.DeserializeObject<Cart>(cartData);
                            if (cart.CreatedBy.ToString() == user.Id)
                            {
                                var cartDetail = new CartDetailDTO();
                                cartDetail.CartId = cart.Id;
                                cartDetail.UserId = new Guid(user.Id);
                                cartDetail.Quantity = 1;
                                cartDetail.ProductId = infoProduct.ProductId;
                                string jsonCartDetail = JsonConvert.SerializeObject(cartDetail);
                                HttpContent content = new StringContent(jsonCartDetail, Encoding.UTF8, "application/json");
                                HttpResponseMessage details = _client.PostAsync(detailURi , content).Result;
                            }

                        }
                        else
                        {

                        }
                    }
                }
            }
            var id = productInfoId.ToString();

            return RedirectToAction("Index");
        }
        public IActionResult Index([FromQuery] Guid productId)
        {
            var product = new ProductViewModel();
            var infoProduct = new List<ProductInfoViewModel>();
            HttpResponseMessage respone = _client.GetAsync(productUri + "/GetProductById" + $"/{productId}").Result;
            HttpResponseMessage infoRespone = _client.GetAsync(infoUri + "/GetListProductInfoByProduct" + $"/{productId}").Result;
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
