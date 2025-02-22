﻿using Meally.core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Meally.core.Service.Contract
{
    public interface IAuthService
    {
        Task<string> CreateTokenAsync(AppUser user,UserManager<AppUser> userManager);
        Task<string> AddRoleAsync(AddRoleModelDto model);
    }
}
