using System.ComponentModel.DataAnnotations;

namespace Meally.API.Dtos
{
    public class OrderItemDto
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public string MealPictureUrl { get; set; }
        public string MealComponents { get; set; }
        [Range(20, 350)]
        public int MealCalories { get; set; }
        public string MealType { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime DeliveryDate { get; set; }

    }
}
