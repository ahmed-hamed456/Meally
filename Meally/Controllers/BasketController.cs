using AutoMapper;
using Meally.API.Dtos;
using Meally.API.Errors;
using Meally.core.Entities.Identity;
using Meally.core.Repository.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meally.API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepo,IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket  = await _basketRepo.GetBasketAsync(id);
            return basket ?? new CustomerBasket(id);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto,CustomerBasket>(basket);

            var CreatedOrupdatedBasket = await _basketRepo.UpdateBasketAsync(mappedBasket);

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
