using Meally.core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Service.Contract
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string userId, int subscriptionId, NumberOfMeals numberOfMeals, string basketId,Address address);

        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string userId);

        Task<Order> GetOrderByIdForUserAsync(int orderId, string userId); 
    }
}
