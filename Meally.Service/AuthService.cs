using Meally.core.Entities.Identity;
using Meally.core.Service.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Meally.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(
            IConfiguration configuration,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> AddRoleAsync(AddRoleModelDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User is alreadu assign to this role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded? string.Empty:"Something went wrong"; 
        }

        public async Task<string> CreateTokenAsync(AppUser user,UserManager<AppUser> userManager)
        {
            //Private claims
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,user.UserName),
                new Claim(ClaimTypes.Email,user.Email)
            };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            //Secret key
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            //Token
            var token = new JwtSecurityToken
                (
                    audience : _configuration["JWT:ValidAudience"],
                    issuer : _configuration["JWT:ValidIssuer"],
                    expires : DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                    claims: authClaims,
                    signingCredentials : new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
           
        }
    }
}
