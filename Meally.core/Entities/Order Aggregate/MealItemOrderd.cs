using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Entities.Order_Aggregate
{
    public class MealItemOrderd
    {
        public Guid Id { get; set; }
        public string MealName { get; set; }
        public string MealPictureUrl { get; set; }
        public string MealComponents { get; set; }
        [Range(20,350)]
        public string MealCalories { get; set; }
        public string MealType { get; set; }
    }
}
