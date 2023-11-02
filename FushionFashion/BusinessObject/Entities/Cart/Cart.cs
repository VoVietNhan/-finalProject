using BusinessObject.Enum.EnumStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entities.Cart
{
    public class Cart : BaseEntity
    {
        public EnumStatus Status { get; set; }
        public ICollection<CartDetail>? CartDetails { get; set; } = new List<CartDetail>();
    }
}
