using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dtos.Cart
{
    public class CartItemDTO
    {
        public Guid ProductId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public Decimal? Price { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }

    }
}
