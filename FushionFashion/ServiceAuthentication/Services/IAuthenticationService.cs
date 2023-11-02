using BusinessObject.Dtos.Account;
using System.Threading.Tasks;

namespace ServiceAuthentication.Services
{
    public interface IAuthenticationService
    {
        public Task<RegisterDtos> RegisterUser(RegisterDtos registerDtos);
    }
}