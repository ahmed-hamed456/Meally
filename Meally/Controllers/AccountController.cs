using AutoMapper;
using Meally.API.Dtos;
using Meally.API.Errors;
using Meally.API.Extensions;
using Meally.core.Entities.Identity;
using Meally.core.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Mail;
using System.Security.Claims;

namespace Meally.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IMailingService _mailingService;
        private readonly IMemoryCache _memoryCache;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IAuthService authService,
            IMapper mapper,
            IMailingService mailingService,
            IMemoryCache memoryCache
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
            _mailingService = mailingService;
            _memoryCache = memoryCache;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized(new ApiResponse(401));

            var pass = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (pass.Succeeded is false)
                return Unauthorized(new ApiResponse(401));

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return BadRequest("Email not confirmed");

            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExists(model.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "This email is already used" } });

            if (model.ConfirmPassword == model.Password)
            {
                var user = new AppUser()
                {
                    DisplayName = model.DisplayName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email.Split("@")[0]
                    //UserName = new MailAddress(model.Email).User //(Second shape)
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                Random confirmationCode = new Random();

                string code = confirmationCode.Next(100000, 999999).ToString();

                await _mailingService.SendEmailAsync(model.Email, code);

                await _userManager.SetAuthenticationTokenAsync(user, "Confirmation", "Code", code);

                if (!result.Succeeded) return BadRequest(new ApiResponse(400, "a problem with this user"));

                //Add User Role
                await _userManager.AddToRoleAsync(user, ConstantsRole.User);

                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await _authService.CreateTokenAsync(user, _userManager)
                };
            }
            else
                return BadRequest(new ApiResponse(400, "a problem with your confirm password"));
        }

        [Authorize]
        [HttpPost("confirm")]
        public async Task<IActionResult> Confirmation(ConfirmDto Dto)
        {
            if (_memoryCache.TryGetValue("VerificationCode", out string storedCode))
            {
                if (Dto.ConfirmationCode == storedCode)
                {
                    var email = User.FindFirstValue(ClaimTypes.Email);

                    var user = await _userManager.FindByEmailAsync(email);

                    var isCodeValid = await _userManager.GetAuthenticationTokenAsync(user, "Confirmation", "Code");

                    if (Dto.ConfirmationCode != isCodeValid)
                    {
                        return Ok("Email confirmation failed");
                    }

                    var result = await _userManager.ConfirmEmailAsync(user, await _userManager.GenerateEmailConfirmationTokenAsync(user));

                    if (result.Succeeded)
                    {
                        _memoryCache.Remove("VerificationCode");

                        return Ok("Verification code confirmed successfully.");
                    }
                    else
                    {
                        return BadRequest("Email confirmation failed.");
                    }
                }
            }
            return BadRequest("Click On ResendCode");
        }

        [Authorize]
        [HttpPost("ResendCode")]
        public async Task<IActionResult> ResendCode()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            Random confirmationCode = new Random();

            string code = confirmationCode.Next(100000, 999999).ToString();

            await _mailingService.SendEmailAsync(email, code);

            await _userManager.SetAuthenticationTokenAsync(user, "Confirmation", "Code", code);

            return Ok($" Anthor Code ==> {code}");
        }

        [HttpPost("forgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            
            if(user != null)
            {
                await _userManager.RemoveAuthenticationTokenAsync(user, "Confirmation", "Code");

                Random confirmationCode = new Random();

                string code = confirmationCode.Next(100000,999999).ToString();

                await _mailingService.SendEmailAsync(model.Email, code);

                await _userManager.SetAuthenticationTokenAsync(user, "Confirmation", "Code", code);

                return Ok($" Code ==> {code}");
            }
            else
                return BadRequest($"{model.Email} not found in Database ");
        }

        [Authorize]
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto model)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Reset the user's password
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok("Password reset successfully.");
            }

            return BadRequest("Password reset failed.");
        }

        [Authorize(Roles = ConstantsRole.Admin)]
        [HttpPost("addrole")]
        public async Task<ActionResult> AddRoleAsync(AddRoleModelDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressAsync(User);

            var address = _mapper.Map<AddressDto>(user.Address);

            return Ok(address);
        }

        [Authorize]
        [HttpPost("createAddress")]
        public async Task<ActionResult<AddressDto>> CreateAddress(AddressDto CreatedAddress)
        {
            var address = _mapper.Map<AddressDto, Address>(CreatedAddress);

            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            address.AppUserId = user.Id;

            user.Address = address;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return CreatedAddress;
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto updatedAddress)
        {
            var address = _mapper.Map<AddressDto, Address>(updatedAddress);

            var user = await _userManager.FindUserWithAddressAsync(User);

            address.Id = user.Address.Id;

            user.Address = address;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(updatedAddress);
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

    }
}



