using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Entities.Identity
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public string RestaurantName { get; set; }
        public string Components { get; set; }
        public string Type { get; set; }
        public int Calories { get; set; }


        public ICollection<CustomerBasketItem> CustomerBasketItems { get; set; }
    }
}
