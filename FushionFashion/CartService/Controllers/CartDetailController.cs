using BusinessObject.Entities.Cart;
using CartService.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailController : ControllerBase
    {
        private readonly CartContext _context;
        public CartDetailController(CartContext cartContext) { 
            _context = cartContext;
        }
        [HttpGet]
        public async  Task<List<CartDetail>> GetAllCartDetailByUserId(Guid UserId) {
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.CreatedBy == UserId);
            var listCartDetail = await _context.CartDetails.Where(x=> x.CreatedBy == cart.Id).ToListAsync();
            return listCartDetail;
        }
/*        [HttpPost("CreateCartDetai")]*/
/*        public async Task<CartDetail> CreateCartDetail(CartDetailDTO cartDetaiDto)
        {

        }*/
    }
}
