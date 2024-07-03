using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public byte[]? Profile_Picture { get; set; }
        public int AddressId { get; set; }
        public ICollection<Address> Address { get; set; }

        public ICollection<CustomerBasket> CustomerBaskets { get; set; }
    }
}
