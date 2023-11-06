using BusinessObject.Enum.EnumStatus;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Dtos.Account
{
    public class RegisterDtos
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Fullname is required")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }
}
