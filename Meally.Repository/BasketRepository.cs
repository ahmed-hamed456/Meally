using Meally.core.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Meally.Repository.Identity;
using Meally.core.Entities.Identity;

namespace Meally.Repository
{
    public class BasketRepository :IBasketRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly AppIdentityDbContext _context;
        public BasketRepository(IMemoryCache memoryCache, AppIdentityDbContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _memoryCache.GetOrCreate(basketId, entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromDays(1));
                return Task.Run(()=>new CustomerBasket("8541451") );
                });

            return basket ?? null ;
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var updateorcreatebasket =  _memoryCache.Set(basket.Id, basket);

            return updateorcreatebasket ?? null;
        }

        public Task DeleteBasketAsync(string basketId)
        {
           return Task.Run(()=>_memoryCache.Remove(basketId));
        }
    }
}
