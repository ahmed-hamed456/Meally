using AutoMapper;
using Meally.API.Dtos;
using Meally.core.Entities.Identity;
using System.Text;

namespace Meally.API.Helpers
{
    public class ProfilePictureUrlForUser : IValueResolver<AppUser, UserDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProfilePictureUrlForUser(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(AppUser source, UserDto destination, string destMember, ResolutionContext context)
        {
            if (source.Profile_Picture != null)
            {
                string result = Encoding.UTF8.GetString(source.Profile_Picture);
                if (!string.IsNullOrEmpty(result))
                {
                    return $"{_configuration["ApiBaseUrl"]}/{result}";
                }
            }
            return string.Empty;
        }
    }
}
