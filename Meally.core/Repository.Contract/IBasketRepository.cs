using Meally.core.Entities.Identity;

namespace Meally.core.Repository.Contract;

public interface IBasketRepository
{
    Task<CustomerBasket?> GetBasketAsync(string basketId);
    Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
    Task DeleteBasketAsync(string basketId);
}