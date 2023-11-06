using BusinessObject.Enum.EnumStatus;
using System;

namespace BusinessObject.Dtos.Product
{
    public class CreateProductViewModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public EnumStatus Status { get; set; } = EnumStatus.Enable;
        public Guid CategoryId { get; set; } = Guid.NewGuid();
    }
}
