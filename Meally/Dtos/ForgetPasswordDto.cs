using System.ComponentModel.DataAnnotations;

namespace Meally.API.Dtos
{
    public class ForgetPasswordDto
    {
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})$")]
        public string Email { get; set; }
    }
}
