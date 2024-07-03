using Meally.core.Entities.Order_Aggregate;

namespace Meally.API.Dtos
{
    public class OrderToReturnDto
    {
        public string UserId { get; set; }
        public DateTimeOffset CreationDate { get; set; } 
        public Address ShappingAddress { get; set; }
        public int NumOfMeals { get; set; }
        public string Status { get; set; } 
        public decimal TotalPrice { get; set; }
        public int SubscriptionId { get; set; }
        public string Subscription { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
    }
}
