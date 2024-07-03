using Meally.core.Entities.Identity;

namespace Meally.core.Repository.Contract;

public interface IBasketRepository
{
    CustomerBasket GetBasketAsync(string basketId);
    Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket,string userId);
    Task DeleteBasketAsync(string basketId);
}