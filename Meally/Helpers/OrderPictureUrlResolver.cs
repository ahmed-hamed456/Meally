using AutoMapper;
using Meally.API.Dtos;
using Meally.core.Entities.Order_Aggregate;

namespace Meally.API.Helpers
{
    public class OrderPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Meal.MealPictureUrl))
                return $"{_configuration["ApiBaseUrl"]}/{source.Meal.MealPictureUrl}";

            return string.Empty;
        }
    }
}
