using AutoMapper;
using Meally.API.Dtos;
using Meally.API.Errors;
using Meally.core.Entities.Identity;
using Meally.core.Entities.Order_Aggregate;
using Meally.core.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Address = Meally.core.Entities.Order_Aggregate;


namespace Meally.API.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public OrdersController(IOrderService orderService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _orderService = orderService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            var userId = user.Id;

            var address = _mapper.Map<OrderAddressDto, Address.Address>(orderDto.ShappingAddress);

            var order = await _orderService.CreateOrderAsync(userId, orderDto.SubscriptionId, orderDto.NumberOfMeals, orderDto.BasketId, address);

            if (order is null) return BadRequest(new ApiResponse(400));

            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }
    }
}
