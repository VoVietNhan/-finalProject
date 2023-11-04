using AutoMapper;
using BusinessObject.Dtos.Account;
using BusinessObject.Dtos.Category;
using BusinessObject.Dtos.Order;
using BusinessObject.Dtos.Product;
using BusinessObject.Dtos.ProductInfo;
using BusinessObject.Dtos.Size;
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

            CreateMap<Product, CreateProductViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Product, UpdateProductViewModel>().ReverseMap();

            CreateMap<Order,OrderDTO>().ReverseMap();

            CreateMap<ProductInfo, ProductInfoViewModel>().ReverseMap();
            CreateMap<ProductInfo, CreateProductInfoViewModel>().ReverseMap();
            CreateMap<ProductInfo, UpdateProductInfoViewModel>().ReverseMap();

            CreateMap<Category, CategoryViewModel>().ReverseMap();  
            CreateMap<Category, CreateCategoryViewModel>().ReverseMap();  

            CreateMap<Size, SizeViewModel>().ReverseMap(); 
            CreateMap<Size, CreateSizeViewModel>().ReverseMap(); 
            CreateMap<Size, UpdateSizeViewModel>().ReverseMap(); 
        }
    }
}
