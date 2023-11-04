using AutoMapper;
using BusinessObject.Dtos.Account;
using BusinessObject.Dtos.Order;
using BusinessObject.Dtos.Product;
using BusinessObject.Entities;
using BusinessObject.Entities.Account;
using BusinessObject.Entities.Product;

namespace BusinessObject.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AppUser, RegisterDtos>().ReverseMap();
            CreateMap<AppUser, LoginDtos>().ReverseMap();
            CreateMap<AppUser, UserDtos>().ReverseMap();

            CreateMap<Product, CreateProductViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Product, UpdateProductViewModel>().ReverseMap();

            CreateMap<Order,OrderDTO>().ReverseMap();
        }
    }
}
