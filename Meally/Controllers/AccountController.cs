using AutoMapper;
using Meally.API.Dtos;
using Meally.API.Errors;
using Meally.API.Extensions;
using Meally.core;
using Meally.core.Entities.Identity;
using Meally.core.Service.Contract;
using Meally.Repository.Data;
using Meally.Repository.Data.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

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
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppIdentityDbContext _context;
        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IAuthService authService,
            IMapper mapper,
            IMailingService mailingService,
            IMemoryCache memoryCache
,
            IUnitOfWork unitOfWork,
            AppIdentityDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
            _mailingService = mailingService;
            _memoryCache = memoryCache;
            _unitOfWork = unitOfWork;
            _context = context;
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


            var result = _mapper.Map<UserDto>(user);

            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Profile_Picture = result.Profile_Picture,
                Token = await _authService.CreateTokenAsync(user, _userManager),
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisteredUserDto>> Register(RegisterDto model)
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
                    UserName = model.Email.Split("@")[0],
                    //UserName = new MailAddress(model.Email).User //(Second shape)
                    //Profile_Picture= model.PictureUrl,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                Random confirmationCode = new Random();

                string code = confirmationCode.Next(100000, 999999).ToString();

                await _mailingService.SendEmailAsync(model.Email, code);

                await _userManager.SetAuthenticationTokenAsync(user, "Confirmation", "Code", code);

                if (!result.Succeeded) return BadRequest(new ApiResponse(400, "a problem with this user"));

                //Add User Role
                await _userManager.AddToRoleAsync(user, ConstantsRole.User);

                return new RegisteredUserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    //PictureUrl = user.PictureUrl,
                    Token = await _authService.CreateTokenAsync(user, _userManager)
                };
            }
            else
                return BadRequest(new ApiResponse(400, "a problem with your confirm password"));
        }

        [Authorize]
        [HttpPost("SetProfilePicture")]
        public async Task<IActionResult> SetProfilePicture([FromForm] ProfilePictureDto Dto)
        {
            if (Dto.profile_Picture == null || Dto.profile_Picture.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            string fileName;
            do
            {
                fileName = GenerateFileNameWithoutNumbers(Dto.profile_Picture.FileName);
            } while (System.IO.File.Exists(Path.Combine("wwwroot/Users/", fileName)));

            var filePath = Path.Combine("wwwroot/Users/", fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await Dto.profile_Picture.CopyToAsync(fileStream);
            }

            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            if (Dto.profile_Picture != null)
            {
                user.Profile_Picture = Encoding.UTF8.GetBytes($"Users/{fileName}");
            }


             await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet()]
        private string GenerateFileNameWithoutNumbers(string originalFileName)
        {
            var guid = Guid.NewGuid().ToString("N");
            var extension = Path.GetExtension(originalFileName);
            var fileNameWithoutNumbers = Regex.Replace(guid.Substring(0, 6), @"\d", "a");
            return $"{fileNameWithoutNumbers}{extension}";
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
            if (CreatedAddress is null)
                return BadRequest("Invalid address data");
            
            var address = _mapper.Map<AddressDto,Address> (CreatedAddress);

            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return BadRequest("User not found");
            
            address.AppUserId = user.Id;

            if (user.Address == null)
                user.Address = new List<Address>();
            
            user.Address.Add(address);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return CreatedAddress;
        }

        [Authorize]
        [HttpPost("calories")]
        public async Task<ActionResult<CaloriesDto>> CalculateCalories(int calories)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            var userId = user.Id;

            var userCalories = new UserCalories(userId, calories);


            await _unitOfWork.Repository<UserCalories>().AddEntity(userCalories);


            var result = await _unitOfWork.CompeleteAsync();

            if (result <= 0) return null;

            return new CaloriesDto
            {
                UserId = userId,
                Calories = calories
            };
        }

        [HttpPut("edit")]
        public async Task<ActionResult<EditedDataDto>> EditPersonalData(EditedDataDto dataDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            //user.Profile_Picture = dataDto.PictureUrl;
            user.DisplayName = dataDto.DisplayName;
            user.PhoneNumber = dataDto.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return dataDto;
        }

        [Authorize]
        [HttpPost("mealTimes")]
        public async Task<ActionResult> SetMealTimes(MealTimesDto Dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            var userId = user.Id;

            var mealTimes = new MealTimes
            {
                UserId = userId,
                BreakfastTime = Dto.BreakfastTime,
                LunchTime = Dto.LunchTime,
                DinnerTime = Dto.DinnerTime,
            };

            await _unitOfWork.Repository<MealTimes>().AddEntity(mealTimes);

            var result = await _unitOfWork.CompeleteAsync();

            if (result <= 0) return null;

            return Ok(new MealTimesDto
            {
                BreakfastTime = Dto.BreakfastTime,
                LunchTime = Dto.LunchTime,
                DinnerTime = Dto.DinnerTime,
            });
        }


        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }


    }
}



