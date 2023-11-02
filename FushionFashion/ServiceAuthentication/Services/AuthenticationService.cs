using AutoMapper;
using BusinessObject.Dtos.Account;
using BusinessObject.Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceAuthentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AuthenticationService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<RegisterDtos> RegisterUser(RegisterDtos registerDtos)
        {
            var identityUser = new AppUser
            {
                UserName = registerDtos.Username,
                Email = registerDtos.Username,
                PhoneNumber = registerDtos.Phone,
                Address = registerDtos.Address,
                Fullname = registerDtos.Fullname,
                Status = BusinessObject.Enum.EnumStatus.EnumStatus.Enable
            };
            var result = await _userManager.CreateAsync(identityUser, registerDtos.Password);
            return null;
        }
    }
}
