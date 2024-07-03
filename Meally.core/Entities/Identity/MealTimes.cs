using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Entities.Identity
{
    public class MealTimes : BaseEntity
    {
        public string UserId { get; set; }
        public TimeSpan? BreakfastTime { get; set; }
        public TimeSpan? LunchTime { get; set; }
        public TimeSpan? DinnerTime { get; set; }

        //public MealTimes(string userId, TimeSpan? breakfastTime, TimeSpan? lunchTime, TimeSpan? dinnerTime)
        //{
        //    UserId = userId;
        //    BreakfastTime = breakfastTime;
        //    LunchTime = lunchTime;
        //    DinnerTime = dinnerTime;
        //}
    }
}
