using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Dtos.Account
{
    public class LoginDtos
    {
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
