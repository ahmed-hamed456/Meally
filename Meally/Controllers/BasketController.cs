using AutoMapper;
using Meally.API.Dtos;
using Meally.API.Errors;
using Meally.core.Entities.Identity;
using Meally.core.Repository.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Meally.API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public BasketController(IBasketRepository basketRepo, IMapper mapper, UserManager<AppUser> userManager)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
        {
            var basket  =  _basketRepo.GetBasketAsync(basketId);
            return basket ?? new CustomerBasket(basketId);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            var userId = user.Id;

            var mappedBasket = _mapper.Map<CustomerBasketDto,CustomerBasket>(basket);

            var CreatedOrupdatedBasket = await _basketRepo.UpdateBasketAsync(mappedBasket,userId);

            if (CreatedOrupdatedBasket is null) return BadRequest(new ApiResponse(400));

            return Ok(CreatedOrupdatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
             await _basketRepo.DeleteBasketAsync(id);
        }
    }
}
