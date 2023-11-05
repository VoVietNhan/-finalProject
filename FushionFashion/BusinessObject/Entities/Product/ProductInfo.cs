using BusinessObject.Enum.EnumStatus;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entities.Product
{
    public class ProductInfo : BaseEntity
    {
        public int Quantity { get; set; }
        public Guid SizeId { get; set; }
        public  Size? Size { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public EnumStatus Status { get; set; }
    }
}
