
using BusinessObject.Enum.EnumStatus;
using System;
using System.Collections.Generic;

namespace BusinessObject.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public EnumStatus Status { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? ArriveDate { get; set; }
        public ICollection<OrderDetails>? OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
