using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Entities.Identity
{
    public class CustomerBasketItem
    {
        public int Id { get; set; }
        public string CustemerBasketId { get; set; }
        public CustomerBasket CustomerBasket { get; set; }

        public int BasketItemId { get; set; }
        public BasketItem BasketItem { get; set; }

    }
}
