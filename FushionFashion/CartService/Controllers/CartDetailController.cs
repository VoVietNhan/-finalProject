using BusinessObject.Dtos.Cart;
using BusinessObject.Dtos.CartDetail;
using BusinessObject.Entities.Cart;
using BusinessObject.Entities.Product;
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
        private readonly CartController cartController;
        public CartDetailController(CartContext cartContext) { 
            _context = cartContext;
        }
        [HttpGet]
        public async  Task<List<CartDetail>> GetAllCartDetailByUserId(Guid UserId) {
            var listCartDetail = await _context.CartDetails.Where(x=> x.CreatedBy == UserId).ToListAsync();
            return listCartDetail;
        }
        [HttpPost("CreateCartDetai")]
        public async Task<CartDetail> CreateCartDetail(CartDetailDTO cartDetaiDto)
        {
            var cartDetail = new CartDetail();
            cartDetail.CartId = cartDetaiDto.CartId;
            cartDetail.ProductId = cartDetaiDto.ProductId;
            cartDetail.Quantity = cartDetaiDto.Quantity;
            cartDetail.CreatedBy = cartDetaiDto.UserId;
            await _context.CartDetails.AddAsync(cartDetail);
            await _context.SaveChangesAsync();
            return cartDetail;
        }
        [HttpPost("UpdateQuantity")]
        public async Task UpdateCartQuantity([FromBody]UpdateDetailDTO  update)
        {
            var cartDetail = await _context.CartDetails.FirstOrDefaultAsync(x => x.ProductId == update.ProductId);
            if (update.Action == "up")
            {
                cartDetail.Quantity += 1;
               await _context.SaveChangesAsync();
                return;
            }
            else
            {
                cartDetail.Quantity -= 1;
                _context.SaveChangesAsync();
                return;
            }
        }
        [HttpPost("Up")]
        public async Task Up([FromBody]Guid ProductID)
        {
            var cartDetail = await _context.CartDetails.FirstOrDefaultAsync(x => x.ProductId == ProductID);

                cartDetail.Quantity += 1;
                await _context.SaveChangesAsync();
                return;
           
        }
        [HttpDelete("DeleteCartItem")]
        public async Task DeleteCartDetailItem(Guid ProductId)
        {
            var item = await _context.CartDetails.FirstOrDefaultAsync(x => x.ProductId.Equals(ProductId));
            if (item == null)
            {
                return;
            }
            else
            {
                _context.CartDetails.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
