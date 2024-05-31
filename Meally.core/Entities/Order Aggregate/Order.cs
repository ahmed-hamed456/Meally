using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Entities.Order_Aggregate;
    public class Order : BaseEntity
{
    public string UserId { get; set; }
    public DateTime CreationDateOrder { get; set; }
    public Address ShappingAddress { get; set; }
    public NumberOfMeals NumOfMeals { get; set; }
    public OrderStatus Status { get; set; }
    public Guid SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

    public Order()
    {
        CreationDateOrder = DateTime.UtcNow;
    }
}
    

