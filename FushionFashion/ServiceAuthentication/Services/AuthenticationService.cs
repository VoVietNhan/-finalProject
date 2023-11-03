using AutoMapper;
using BusinessObject.Dtos.Account;
using BusinessObject.Entities.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
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

        public async Task<AppUser> RegisterUser(RegisterDtos registerDtos)
        {
            var userExits = await _userManager.FindByEmailAsync(registerDtos.Username);
            if (userExits != null)
            {
                throw new Exception("Username already exists!");
            }
            var identityUser = new AppUser
            {
                UserName = registerDtos.Username,
                Email = registerDtos.Username,
                PhoneNumber = registerDtos.Phone,
                Address = registerDtos.Address,
                Fullname = registerDtos.Fullname,
                Status = BusinessObject.Enum.EnumStatus.EnumStatus.Enable
            };
            await _userManager.CreateAsync(identityUser, registerDtos.Password);
            return identityUser;
        }

        public async Task<AppUser> LoginUser(LoginDtos loginDtos)
        {
            var identityUser = await _userManager.FindByEmailAsync(loginDtos.Username);
            if (identityUser == null)
            {
                throw new Exception("Username doesn't exists!");
            }
            await _userManager.CheckPasswordAsync(identityUser, loginDtos.Password);
            return identityUser;
        }
    }
}
