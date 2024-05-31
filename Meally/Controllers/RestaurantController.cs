using Meally.API.Errors;
using Meally.core.Entities;
using Meally.core.MealsSpecs;
using Meally.core.Repository.Contract;
using Meally.core.RestaurantsSpecs;
using Meally.core.Specifications;
using Meally.Repository;
using Meally.Repository.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meally.API.Controllers
{
    public class RestaurantController : BaseApiController
    {
        private readonly IGenericRepository<Restaurant> _restaurantRepo;
        private readonly IGenericRepository<Meal> _mealRepo;
        private readonly IGenericRepository<Category> _categoryRepo;

        public RestaurantController(
            IGenericRepository<Restaurant> restaurantRepo,
            IGenericRepository<Meal> mealRepo,
            IGenericRepository<Category> categoryRepo
            )
        {
            _restaurantRepo = restaurantRepo;
            _mealRepo = mealRepo;
            _categoryRepo = categoryRepo;
        }

        
        [HttpGet("restaurant")]
        public async Task<ActionResult<IReadOnlyList<Restaurant>>> GetAllRestaurants()
        {
            var restaurants = await _restaurantRepo.GetAllAsync();

            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(Guid id)
        {
            var restaurant = await _restaurantRepo.GetEntityAsync(id);

            if (restaurant is null)
                return NotFound(new ApiResponse(404));

            return Ok(restaurant);
        }


        [HttpDelete("restaurant")]
        public async Task<IActionResult> DeleteRestaurant(Guid id)
        {
            var retaurant = await _restaurantRepo.GetEntityAsync(id);

            if (retaurant is null) return NotFound(new ApiResponse(404));

             _restaurantRepo.DeleteEntity(retaurant);

            return Ok();
        }

        [HttpGet("meals")]
        public async Task<ActionResult<IReadOnlyList<Meal>>> GetAllMeals([FromQuery]MealsSpecParams specParams)
        {
            var spec = new MealWithRestaurantWithCategory(specParams);

            var meals = await _mealRepo.GetAllAsyncSpec(spec);

            return Ok(meals);
        }

        [HttpDelete("meals")]
        public async Task<IActionResult> DeleteMeal(Guid id)
        {
            var meal = await _mealRepo.GetEntityAsync(id);

            if (meal is null) return NotFound(new ApiResponse(404));

            _mealRepo.DeleteEntity(meal);

            return Ok();
        }

        [HttpGet("category")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetAllCategory()
        {
            var category = await _categoryRepo.GetAllAsync();

            return Ok(category);
        }

        [HttpDelete("category")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _categoryRepo.GetEntityAsync(id);

            if (category is null) return NotFound(new ApiResponse(404));

            _categoryRepo.DeleteEntity(category);

            return Ok();
        }
    }
}
