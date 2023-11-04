using BusinessObject.Enum.EnumStatus;

namespace BusinessObject.Dtos.Account
{
    public class UserDtos
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public EnumStatus Status { get; set; } 
    }
}
