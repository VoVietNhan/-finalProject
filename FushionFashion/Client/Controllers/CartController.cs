using BusinessObject.Dtos.Account;
using BusinessObject.Dtos.Cart;
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
        Uri detailURi = new Uri("https://localhost:44340/api/CartDetail");
        Uri upUri = new Uri("https://localhost:44340/api/CartDetail");
        Uri deleteUri = new Uri("https://localhost:44340/api/CartDetail");
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
                        HttpResponseMessage detailRespone = _client.GetAsync(detailURi + "?UserId=" + $"{user.Id}").Result;
                        if (detailRespone.IsSuccessStatusCode)
                        {
                            string detailData = detailRespone.Content.ReadAsStringAsync().Result;
                            var cartDetailList = JsonConvert.DeserializeObject<List<CartDetail>>(detailData);
                            foreach (var item in cartDetailList)
                            {
                                if (item.ProductId == infoProduct.Id)
                                {
                                    string jsonQuantity = JsonConvert.SerializeObject(infoProduct.Id);
                                    HttpContent content = new StringContent(jsonQuantity, Encoding.UTF8, "application/json");
                                    HttpResponseMessage update = _client.PostAsync(upUri + "/Up?ProductID=", content).Result;
                                    return RedirectToAction("Index");
                                }
                            }
                            HttpResponseMessage cartRespone = _client.GetAsync(cartUri + $"?UserId={user.Id}").Result;
                            if (cartRespone.IsSuccessStatusCode)
                            {

                                string cartData = cartRespone.Content.ReadAsStringAsync().Result;
                                var cart = JsonConvert.DeserializeObject<Cart>(cartData);
                                if (cart.CreatedBy == user.Id)
                                {

                                    var cartDetail = new CartDetailDTO();
                                    cartDetail.CartId = cart.Id;
                                    cartDetail.UserId = user.Id;
                                    cartDetail.Quantity = 1;
                                    cartDetail.ProductId = productInfoId;
                                    string jsonCartDetail = JsonConvert.SerializeObject(cartDetail);
                                    HttpContent content = new StringContent(jsonCartDetail, Encoding.UTF8, "application/json");
                                    HttpResponseMessage details = _client.PostAsync(detailURi + "/CreateCartDetai", content).Result;
                                }
                            }
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var cartItemList = new List<CartItemDTO>();
            var user = new UserDtos();
            string mail = HttpContext.Session.GetString("Email");
            if (mail != null)
            {
                HttpResponseMessage respone = _client.GetAsync(authentication + "/GetUserByEmail" + $"/{mail}").Result;
                if (respone.IsSuccessStatusCode)
                {
                    string data = respone.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<UserDtos>(data);
                }
            }
            HttpResponseMessage detailRespone = _client.GetAsync(detailURi + "?UserId=" + $"{user.Id}").Result;
            if (detailRespone.IsSuccessStatusCode)
            {
                string detailData = detailRespone.Content.ReadAsStringAsync().Result;
                var cartDetail = JsonConvert.DeserializeObject<List<CartDetail>>(detailData);
                foreach (var item in cartDetail)
                {
                    HttpResponseMessage infoRespone = _client.GetAsync(infoUri + "/GetProductInfoById" + $"/{item.ProductId}").Result;

                    if (infoRespone.IsSuccessStatusCode)
                    {
                        string infoData = infoRespone.Content.ReadAsStringAsync().Result;
                        var infoProduct = JsonConvert.DeserializeObject<ProductInfoViewModel>(infoData);
                        List<ProductViewModel> productList = new List<ProductViewModel>();
                        HttpResponseMessage respone = _client.GetAsync(productUri + "/GetAllProduct").Result;
                        if (respone.IsSuccessStatusCode)
                        {
                            string data = respone.Content.ReadAsStringAsync().Result;
                            productList = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
                            foreach (var product in productList)
                            {
                                if (product.Id == infoProduct.ProductId)
                                {
                                    var carItem = new CartItemDTO();

                                    carItem.ProductId = infoProduct.Id;
                                    carItem.Image = product.Image;
                                    carItem.Name = product.Name;
                                    carItem.Price = product.Price;
                                    carItem.Size = infoProduct.Size.ProSize;
                                    carItem.Quantity = item.Quantity;

                                    cartItemList.Add(carItem);
                                }
                            }
                        }
                    }
                }
            }
            return View(cartItemList);
        }
        [HttpPost]
        public IActionResult UpdateQuantity(Guid ProductId, string Action)
        {
            var updateQuantity = new UpdateDetailDTO();
            updateQuantity.ProductId = ProductId;
            updateQuantity.Action = Action;
            string jsonQuantity = JsonConvert.SerializeObject(updateQuantity);
            HttpContent content = new StringContent(jsonQuantity, Encoding.UTF8, "application/json");
            HttpResponseMessage update = _client.PostAsync(upUri + "/UpdateQuantity", content).Result;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(Guid ProductId)
        {
            HttpResponseMessage delete = _client.DeleteAsync(deleteUri + "/DeleteCartItem?ProductId=" + $"{ProductId}").Result;
            return RedirectToAction("Index");
        }
    }
}
