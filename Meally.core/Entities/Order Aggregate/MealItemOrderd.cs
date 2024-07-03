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
        public int MealId { get; set; }
        public string MealName { get; set; }
        public string MealPictureUrl { get; set; }
        public string MealComponents { get; set; }
        [Range(20,350)]
        public int MealCalories { get; set; }
        public string MealType { get; set; }

        public MealItemOrderd()
        {

        }

        public MealItemOrderd(int mealId, string mealName,string mealPictureUrl, string mealComponents, int mealCalories,string mealType)
        {
            MealId = mealId;
            MealName = mealName;
            MealPictureUrl = mealPictureUrl;
            MealComponents = mealComponents;
            MealCalories = mealCalories;
            MealType = mealType;
        }
    }
}
