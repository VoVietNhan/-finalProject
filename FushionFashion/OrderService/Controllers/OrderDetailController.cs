using BusinessObject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
