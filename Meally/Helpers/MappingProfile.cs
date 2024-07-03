using AutoMapper;
using Meally.API.Dtos;
using Meally.core.Entities.Identity;
using Meally.core.Entities.Order_Aggregate;

namespace Meally.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<OrderAddressDto, core.Entities.Order_Aggregate.Address>();

            CreateMap<AppUser, UserDto>()
                .ForMember(A => A.Profile_Picture, U => U.MapFrom<ProfilePictureUrlForUser>());

            CreateMap<Meal,MealToReturnDto>()
                .ForMember(D => D.Restaurant, O => O.MapFrom(S => S.Restaurant.Name))
                .ForMember(D => D.Category, O => O.MapFrom(S => S.Category.Name))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<PictureUrlResolver>());

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(O => O.Subscription, O => O.MapFrom(s => s.Subscription.PackgeName));

            CreateMap<OrderItem,OrderItemDto>()
                .ForMember(O=>O.MealName,O=>O.MapFrom(M=>M.Meal.MealName))
                .ForMember(O => O.MealId, O => O.MapFrom(M => M.Meal.MealId))
                .ForMember(O => O.MealComponents, O => O.MapFrom(M => M.Meal.MealComponents))
                .ForMember(O => O.MealType, O => O.MapFrom(M => M.Meal.MealType))
                .ForMember(O => O.MealPictureUrl, O => O.MapFrom(M => M.Meal.MealPictureUrl))
                .ForMember(d => d.MealPictureUrl, O => O.MapFrom<OrderPictureUrlResolver>());
        }
    }
}
