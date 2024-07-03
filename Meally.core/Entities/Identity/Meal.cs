using Meally.core.Entities.Order_Aggregate;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meally.core.Entities.Identity;

public class Meal : BaseEntity
{
    public string Name { get; set; }
    public string PictureUrl { get; set; }
    public string Components { get; set; }
    public decimal Price { get; set; }
    public int Calories { get; set; }
    public string Type { get; set; }

    public DateTime CreationDate { get; set; }

    [ForeignKey("Restaurant")]
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public Meal()
    {
        CreationDate = DateTime.UtcNow;
    }

}
