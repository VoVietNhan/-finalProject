using BusinessObject.Enum.EnumStatus;
using Microsoft.AspNetCore.Identity;

namespace BusinessObject.Entities.Account
{
    public class AppUser : IdentityUser 
    {
        public string Fullname { get; set; }
        public string Address { get; set; }
        public EnumStatus Status { get; set; } = EnumStatus.Enable;
    }
}
