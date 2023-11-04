using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entities.Product
{
    public class Size : BaseEntity
    {
        public string? ProSize { get; set; }
        public ICollection<ProductInfo> ProductInfos { get; set; } = new List<ProductInfo>();
        public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
