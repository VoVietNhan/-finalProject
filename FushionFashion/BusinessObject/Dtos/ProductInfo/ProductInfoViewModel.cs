using BusinessObject.Enum.EnumStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dtos.ProductInfo
{
    public class ProductInfoViewModel
    {
        public string? Name { get; set; }
        public EnumStatus Status { get; set; }
    }
}
