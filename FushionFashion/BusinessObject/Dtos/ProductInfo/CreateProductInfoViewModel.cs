using BusinessObject.Enum.EnumStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dtos.ProductInfo
{
    public class CreateProductInfoViewModel
    {
        public int Quantity { get; set; }
        public Guid SizeId { get; set; }
        public Guid ProductId { get; set; }
        public EnumStatus Status { get; set; }
    }
}
