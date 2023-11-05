using BusinessObject.Dtos.Order;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly OrderContext _orderContext;
        public OrderDetailController(OrderContext orderContext) {
            _orderContext = orderContext;
        }
        [HttpGet("GetAll")]
        public async Task<List<OrderDetails>> GetAllOrderDetail(Guid OrderId)
        {
            var orderDetailList = await _orderContext.OrderDetails.Where(x => x.OrderId == OrderId).ToListAsync();
            return orderDetailList;
        }
        [HttpPost("CreateOrderDetail")]
        public async Task CreateOrderDetail(List<OrderDetailsDTO> list)
        {
            foreach (var item in list)
            {
                var orderDetails = new OrderDetails();
                orderDetails.OrderId = item.OrderId;
                orderDetails.ProductId = item.ProductId;
                orderDetails.Quantity = item.Quantity;
                orderDetails.SizeId = item.SizeId;
                await _orderContext.OrderDetails.AddAsync(orderDetails);
                await _orderContext.SaveChangesAsync();
            }
        }
     }
}
