using AutoMapper;
using BusinessObject.Dtos.Account;
using BusinessObject.Entities.Account;

namespace BusinessObject.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AppUser, RegisterDtos>().ReverseMap();
            CreateMap<AppUser, LoginDtos>().ReverseMap();
        }
    }
}
