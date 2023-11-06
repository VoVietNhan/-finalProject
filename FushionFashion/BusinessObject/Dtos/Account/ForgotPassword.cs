using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Dtos.Account
{
    public class ForgotPassword
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
    }
}
