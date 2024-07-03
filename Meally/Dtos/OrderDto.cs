using Meally.core.Entities.Order_Aggregate;

namespace Meally.API.Dtos
{
    public class OrderDto
    {
        public int SubscriptionId { get; set; }
        public NumberOfMeals NumberOfMeals { get; set; }
        public string BasketId { get; set; }
        public OrderAddressDto ShappingAddress { get; set; }
    }
}
