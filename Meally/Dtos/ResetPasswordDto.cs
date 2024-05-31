using System.ComponentModel.DataAnnotations;

namespace Meally.API.Dtos
{
    public class ResetPasswordDto
    {
        [Required]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,32}$", ErrorMessage = "You must match regex At least one digit [0-9]\r\nAt least one lowercase character [a-z]\r\nAt least one uppercase character [A-Z] At least 8 characters in length, but no more than 32.")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Password must match confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
