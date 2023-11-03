using BusinessObject.Entities.Cart;
using CartService.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartContext _cartContext;
        public CartController(CartContext cartContext)
        {
            _cartContext = cartContext;
        }
        [HttpGet]
        public async Task<ActionResult<Cart>> GetCartByUserId(Guid UserId)
        {
            var cart =  await _cartContext.Carts.FirstOrDefaultAsync(x => x.CreatedBy == UserId);
            return cart;
        }
        [HttpPost("CreateCart")]
        public async Task<ActionResult<Cart>> CreateCartByUserId(Guid UserId)
        {
            var checkCart = await GetCartByUserId(UserId);
            if(checkCart.Result == null)
            {
                var cart = new Cart();
                cart.CreatedBy = UserId;
                cart.Status = BusinessObject.Enum.EnumStatus.EnumStatus.Enable;
                await _cartContext.Carts.AddAsync(cart);
                await _cartContext.SaveChangesAsync();
                return cart;
            }
            return checkCart;
        }
    }
}
