using BusinessObject.Enum.EnumStatus;

namespace ServiceProduct.ViewModels
{
    public class ProductViewModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public EnumStatus Status { get; set; }
    }
}
