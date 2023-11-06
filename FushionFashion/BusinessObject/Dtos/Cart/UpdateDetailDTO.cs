using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dtos.Cart
{
    public class UpdateDetailDTO
    {
        public string Action { get; set; }
        public Guid ProductId { get; set; }
    }
}
