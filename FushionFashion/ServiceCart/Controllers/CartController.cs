using BusinessObject.Entities.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceCart.Data;
using ServiceCart.Services;
using System;
using System.Threading.Tasks;

namespace ServiceCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartContext _cartContext;
        private readonly IClaimService _claimService;

        public CartController(CartContext cartContext, IClaimService claimService)
        {
            _cartContext = cartContext;
            _claimService = claimService;
        }
        [HttpPost("CreateCart")]
        public async Task<ActionResult<Cart>> CreateCartByUserId(Guid UserId)
        {
            var checkCart = await GetCartByUserId(UserId);
            if(checkCart.Result == null)
            {
                var cart = new Cart();
                cart.Status = BusinessObject.Enum.EnumStatus.EnumStatus.Enable;
                cart.CreatedBy = UserId;
                cart.CreatedDate = DateTime.Now;
                cart.ModifiedBy = UserId;
                cart.ModifiedDate = DateTime.Now;
                cart.DeleteBy = UserId;
                cart.DeleteDate = DateTime.Now;
                await _cartContext.Carts.AddAsync(cart);
                await _cartContext.SaveChangesAsync();
                return cart;
            }
            return checkCart;

        }
        [HttpGet("GetCartById")]
        public async Task<ActionResult<Cart>> GetCartByUserId(Guid UserId)
        {

            var cart =await _cartContext.Carts.FirstOrDefaultAsync(x => x.CreatedBy == UserId);
            return cart;
        }
    }
}
