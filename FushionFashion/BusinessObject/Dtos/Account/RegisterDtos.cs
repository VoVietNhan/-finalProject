using BusinessObject.Enum.EnumStatus;

namespace BusinessObject.Dtos.Account
{
    public class RegisterDtos
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
