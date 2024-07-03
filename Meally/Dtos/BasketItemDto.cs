using System.ComponentModel.DataAnnotations;

namespace Meally.API.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string MealName { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero!!")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least one item!!")]
        public int Quantity { get; set; }
        [Required]
        public string Restaurant { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Components { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Calories { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }

    }
}
