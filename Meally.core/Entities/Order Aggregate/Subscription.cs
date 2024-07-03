using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Meally.core.Entities.Identity;

namespace Meally.core.Entities.Order_Aggregate
{
    public class Subscription : BaseEntity
    {
        public DateTime CreationDate { get; set; }
        public string PackgeName { get; set; }
        public int TotalPrice { get; set; }
        public int NumberOfDays { get; set; }

        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        public Subscription()
        {

        }

    }
    
}
