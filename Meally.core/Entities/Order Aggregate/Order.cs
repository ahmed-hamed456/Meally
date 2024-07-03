using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meally.core.Entities.Identity;

namespace Meally.core.Entities.Order_Aggregate;
public class Order : BaseEntity
{
    public string UserId { get; set; }
    public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;
    public Address ShappingAddress { get; set; }
    public NumberOfMeals NumOfMeals { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalPrice { get; set; } 
    public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

    public Order()
    {

    }

    public Order(string userId, Address shappingAddress, NumberOfMeals numOfMeals, decimal totalPrice, Subscription subscription, ICollection<OrderItem> items)
    {
        UserId = userId;
        ShappingAddress = shappingAddress;
        NumOfMeals = numOfMeals;
        TotalPrice = totalPrice;
        Subscription = subscription;
        Items = items;
    }
}
    

