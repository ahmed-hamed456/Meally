using Meally.core.Entities;
using Meally.core.RestaurantsSpecs;
using Meally.core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.MealsSpecs
{
    public class MealWithRestaurantWithCategory : BaseSpecification<Meal>
    {
        public MealWithRestaurantWithCategory(MealsSpecParams specParams)
            : base(M =>
                     (string.IsNullOrEmpty(specParams.Search) || M.Name.ToLower().Contains(specParams.Search)) &&
                     (!specParams.CategoryId.HasValue || M.RestaurantId == specParams.RestaurantId)&&
                     (!specParams.CategoryId.HasValue || M.CategoryId == specParams.CategoryId) 
                    
                 )
        {
            AddIncludes();

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                AddOrderBy(M => M.Name);
            }

        }

        public MealWithRestaurantWithCategory(Guid id)
                : base(M => M.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(P => P.Restaurant);
            Includes.Add(P => P.Category);
        }
    }
}
