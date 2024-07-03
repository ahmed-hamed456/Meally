namespace Meally.API.Dtos
{
    public class MealToReturnDto
    {
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string Components { get; set; }
        public decimal Price { get; set; }
        public int Calories { get; set; }
        public string Type { get; set; }
        public int RestaurantId { get; set; }
        public int CategoryId { get; set; }
        public string Restaurant { get; set; }
        public string Category { get; set; }

    }
}
