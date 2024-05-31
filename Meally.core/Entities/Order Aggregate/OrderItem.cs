using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Entities.Order_Aggregate
{
    public class OrderItem : BaseEntity
    {
        public MealItemOrderd Meal { get; set; }

        public decimal Price { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int Quantity { get; set; }
    }
}
