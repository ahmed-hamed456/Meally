using Meally.core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Meally.Repository.Data
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> _userManager,AppIdentityDbContext _dbContext)
        {
            if (_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    DisplayName = "Ahmed Hamed",
                    Email = "ahmedhamed20042003@gmail.com",
                    PhoneNumber = "01055481277",
                    UserName = "Ahmed_hamed"
                };
                await _userManager.CreateAsync(user, "Pa$$w0rd");
            }

            if (_dbContext.Categories.Count() == 0)
            {
                var categoryData = File.ReadAllText("../Meally.Repository/Data/DataSeed/categories.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);

                if (categories?.Count() > 0)
                {
                    _dbContext.Set<Category>().AddRange(categories);
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (_dbContext.Restaurants.Count() == 0)
            {
                var retaurantData = File.ReadAllText("../Meally.Repository/Data/DataSeed/restaurants.json");
                var restaurants = JsonSerializer.Deserialize<List<Restaurant>>(retaurantData);

                if (restaurants?.Count() > 0)
                {
                    _dbContext.Set<Restaurant>().AddRange(restaurants);
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (_dbContext.Meals.Count() == 0)
            {
                var mealData = File.ReadAllText("../Meally.Repository/Data/DataSeed/meals.json");
                var meals = JsonSerializer.Deserialize<List<Meal>>(mealData);

                if (meals?.Count() > 0)
                {

                    _dbContext.Set<Meal>().AddRange(meals);

                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
