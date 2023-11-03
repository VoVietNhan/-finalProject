using BusinessObject.Enum.EnumStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dtos.Order
{
    public class OrderDTO
    {
        public DateTime OrderDate { get; set; } = DateTime.Now; 
        public decimal TotalPrice { get; set; }
        public EnumStatus Status { get; set; } = EnumStatus.Enable;
        public Guid UserId { get; set; }
    }
}
