using System.ComponentModel.DataAnnotations;

namespace Meally.API.Dtos
{
    public class UserDto
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Profile_Picture { get; set; }

        public string Token { get; set; }


    }
}
