using BusinessObject.Enum.EnumStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dtos.Cart
{
    public class CartDTO
    {
        public EnumStatus Status { get; set; }
        public ICollection<CartDetail.CartDetailDTO>? CartDetails { get; set; } 
    }
}
