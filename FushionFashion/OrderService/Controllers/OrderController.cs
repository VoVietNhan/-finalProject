using AutoMapper;
using BusinessObject.Dtos.Order;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderContext _orderContext;

        public OrderController(OrderContext orderContext) {
            _orderContext = orderContext;
        }
        [HttpGet("GetAll")]
        public async Task<List<Order>> GetALlOrder()
        {
            var order = await _orderContext.Orders.Include(x=>x.OrderDetails).ToListAsync();
            return order;
        }
        [HttpGet("GetOrder")]
        public async Task<Order> GetOrder (Guid OrderId)
        {
            var order = await _orderContext.Orders.Include(x => x.OrderDetails).FirstOrDefaultAsync(x => x.Id.Equals(OrderId));
            return order;
        }
        [HttpPost("Create")]
        public async Task<Order> CreateOrder(OrderDTO orderDTO)
        {
            var order = new Order();
            order.TotalPrice = orderDTO.TotalPrice;
            order.CreatedBy = orderDTO.UserId;
            await _orderContext.AddAsync(order);
            await _orderContext.SaveChangesAsync();
            return order;
        }
    }
}
