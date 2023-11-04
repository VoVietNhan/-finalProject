using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dtos.Order
{
    public class OrderDetailsDTO
    {
        public int Quantity { get; set; }
        public Guid SizeId { get; set; }
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
