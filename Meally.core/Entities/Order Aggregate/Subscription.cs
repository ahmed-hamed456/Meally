using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Entities.Order_Aggregate
{
    public class Subscription : BaseEntity
    {
        public DateTime CreationDate { get; set; }
        public string PackgeName { get; set; }
        public string TotalPrice { get; set; }
        public string NumberOfDays { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
    
}
