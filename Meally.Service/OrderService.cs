using Meally.core;
using Meally.core.Entities.Identity;
using Meally.core.Entities.Order_Aggregate;
using Meally.core.Repository.Contract;
using Meally.core.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Address = Meally.core.Entities.Order_Aggregate;

namespace Meally.Service
{

    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order?> CreateOrderAsync(string userId, int subscriptionId, NumberOfMeals numberOfMeals, string basketId, Address.Address address)
        {
            // 1. Select Subscribtion
            var subscription = await _unitOfWork.Repository<Subscription>().GetEntityAsync(subscriptionId);

            if (subscription is null) return null;

            // 2. Select Number Of Day Of Subscribtion
            int numberOFMealsInDay = (int)numberOfMeals;

            // 3. Get Basket From Baskets Repo
            var basket = _basketRepo.GetBasketAsync(basketId);

            // 4. Get selected Items at Basket From Meals Repo
            var orderItems = new List<OrderItem>();

            if (basket?.Items?.Count > 0)
            {
                var mealRepository = _unitOfWork.Repository<Meal>();
                foreach (var item in basket.Items)
                {
                    var meal = await mealRepository.GetEntityAsync(item.Id);

                    var mealItemOrdered = new MealItemOrderd(item.Id, meal.Name, meal.PictureUrl, meal.Components, meal.Calories, meal.Type);

                    var orderItem = new OrderItem(mealItemOrdered, meal.Price, item.DeliveryDate, item.Quantity);

                    orderItems.Add(orderItem);
                }
            }

            if (orderItems.Count > (subscription.NumberOfDays * numberOFMealsInDay))
                throw new InvalidOperationException(".لقد قمت باختيار عدد وجبات اكتر من المسموح به فى اشتراكك");

            if (orderItems.Count < (subscription.NumberOfDays * numberOFMealsInDay))
                throw new InvalidOperationException(".لقد قمت باختيار عدد وجبات اقل من المحدد لك فى اشتراكك");

            // 5. Calculate TotalPrice
            var totalPrice = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);

            if (totalPrice > subscription.TotalPrice)
                throw new InvalidOperationException(".لقد قمت باختيار وجبات تفوق قيمة اشتراكك");

            //6. create Order
            var order = new Order(userId, address, numberOfMeals, totalPrice, subscription, orderItems);

            await _unitOfWork.Repository<Order>().AddEntity(order);

            //7. Save To Database
            var result = await _unitOfWork.CompeleteAsync();

            if (result <= 0) return null;

            return order;
        } 

        public Task<Order> GetOrderByIdForUserAsync(int orderId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
