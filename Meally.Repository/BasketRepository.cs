using Meally.core.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Meally.core.Entities.Identity;
using Meally.Repository.Data;
using Org.BouncyCastle.Bcpg;

namespace Meally.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly AppIdentityDbContext _context;
        public BasketRepository(IMemoryCache memoryCache, AppIdentityDbContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }
        

        //public  Task<CustomerBasket?> GetBasketAsync(string basketId)
        //{
        //     //_memoryCache.TryGetValue(basketId, out var basket);
        //    //    async entry =>
        //    //{
        //    //    entry.SetAbsoluteExpiration(TimeSpan.FromDays(1));
        //    //    //(_context.Subscriptions.FirstOrDefault(X => X.Id == _context.Orders.FirstOrDefault(U => U.UserId == userId).SubscriptionId)).NumberOfDays
        //    //    return CreateNewBasket(basketId,items);
        //    //});

        //    //return (Task<CustomerBasket?>)basket;

        //    var basket = _memoryCache.Get(basketId);
        //    return basket;
        //}
        //private CustomerBasket CreateNewBasket(string basketId, List<BasketItem> items)
        //{
        //    return new CustomerBasket()
        //    {
        //        Id= basketId,
        //        Items= items
        //    };
        //}
        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket,string userId)
        {
          var updateorcreatebasket =  _memoryCache.Set(basket.Id, basket, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
               //(_context.Subscriptions.FirstOrDefault(X => X.Id == _context.Orders.FirstOrDefault(U => U.UserId == userId).SubscriptionId)).NumberOfDays

            });

            return updateorcreatebasket ?? null;
        }

        public Task DeleteBasketAsync(string basketId)
        {
           return Task.Run(()=>_memoryCache.Remove(basketId));
        }

        public CustomerBasket GetBasketAsync(string basketId)
        {
            _memoryCache.TryGetValue(basketId, out CustomerBasket basket);
            return basket;
        }
    }
}
