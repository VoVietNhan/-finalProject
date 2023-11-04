using System;

namespace BusinessObject.Entities
{
    public class OrderDetails : BaseEntity
    {
        public int Quantity { get; set; }
        public Guid SizeId { get; set; }
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public Guid ProductId { get; set; }
    }
}
