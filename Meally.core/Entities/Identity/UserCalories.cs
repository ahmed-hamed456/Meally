using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Entities.Identity
{
    public class UserCalories : BaseEntity
    {
        public string UserId { get; set; }
        public int Calories { get; set; }

        public UserCalories(string userId, int calories)
        {
            UserId = userId;
            Calories = calories;
        }
    }
}
