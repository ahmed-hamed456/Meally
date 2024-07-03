using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meally.core.Entities.Identity;

namespace Meally.core.Entities.Order_Aggregate
{
    public class OrderItem : BaseEntity
    {
        public MealItemOrderd Meal { get; set; }

        public decimal Price { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int Quantity { get; set; }

        public OrderItem()
        {

        }

        public OrderItem(MealItemOrderd meal,decimal price, DateTime deliveryDate, int quantity)
        {
            Meal = meal;
            Price = price;
            DeliveryDate = deliveryDate;
            Quantity = quantity;
        }
    }
}
