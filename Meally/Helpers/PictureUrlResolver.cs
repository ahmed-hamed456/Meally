using AutoMapper;
using Meally.API.Dtos;
using Meally.core.Entities.Identity;
using Microsoft.Extensions.Configuration;

namespace Meally.API.Helpers
{
    public class PictureUrlResolver : IValueResolver<Meal, MealToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Meal source, MealToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";

            return string.Empty;
        }
    }
}
