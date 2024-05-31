using System.ComponentModel.DataAnnotations;

namespace Meally.core.Service.Contract
{
    public class AddRoleModelDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }

    }
}