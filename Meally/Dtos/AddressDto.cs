using System.ComponentModel.DataAnnotations;

namespace Meally.API.Dtos
{
    public class AddressDto
    {
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Department { get; set; }
    }
}
