using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dtos.Product
{
    public class CreateProductDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public int Quantity { get; set; }
        public Guid SizeId { get; set; }
    }
}
