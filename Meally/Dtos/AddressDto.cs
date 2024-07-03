using System.ComponentModel.DataAnnotations;

namespace Meally.API.Dtos
{
    public class AddressDto
    {
        [Required]
        public string Region { get; set; }
        [Required]
        public string Building { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Floor { get; set; }
        [Required]
        public string? ApartmentNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string place { get; set; }
        [Required]
        public string? Details { get; set; }
    }
}
