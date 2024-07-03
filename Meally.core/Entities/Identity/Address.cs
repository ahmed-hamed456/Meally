using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public string Building { get; set; }
        public string Street { get; set; }
        public string Floor { get; set; }
        public string? ApartmentNumber { get; set; }
        public string City { get; set; }
        public string place { get; set; }
        public string? Details { get; set; }
        public string AppUserId { get; set; } // Foriegn key
        public AppUser AppUser { get; set; }
    }
}
