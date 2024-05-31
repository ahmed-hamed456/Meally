using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Entities.Identity
{
    public class CustomerBasket
    {
        public string Id { get; set; }

        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<CustomerBasketItem> CustomerBasketItems { get; set; }
        public CustomerBasket(string id)
        {
            Id = id;
        }
    }
}
