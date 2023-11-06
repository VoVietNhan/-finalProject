using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dtos.Account
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "Password should not contain spaces or special characters.")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "Password should not contain spaces or special characters.")]
        public string NewPassword { get; set; }
    }
}
