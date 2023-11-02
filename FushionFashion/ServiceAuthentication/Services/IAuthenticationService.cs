using BusinessObject.Dtos.Account;
using BusinessObject.Entities.Account;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceAuthentication.Services
{
    public interface IAuthenticationService
    {
        public Task<AppUser> RegisterUser(RegisterDtos registerDtos);
        public Task<AppUser> LoginUser(LoginDtos loginDtos);
    }
}