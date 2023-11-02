using AutoMapper;
using BusinessObject.Entities.Product;

namespace BusinessObject.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProductViewModel, Product>().ReverseMap();

            CreateMap<CreateProductViewModel, Product>().ReverseMap();
            CreateMap<UpdateProductViewModel, Product>().ReverseMap();
        }
    }
}
