using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Entities.Identity
{
    [NotMapped]
    public class CustomerBasket
    {
        public string Id { get; set; }
        
        public List<BasketItem> Items { get; set; }
        public CustomerBasket(string id)
        {
            Id = id;
        }

        public CustomerBasket()
        {
        }
    }
}
