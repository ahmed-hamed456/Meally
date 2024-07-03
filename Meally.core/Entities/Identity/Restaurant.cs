using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Entities.Identity
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public decimal Rate { get; set; }
        public string PictureUrl { get; set; }
        public DateTime CreationDate { get; set; }


        public Restaurant()
        {
            CreationDate = DateTime.UtcNow;
        }
    }
}
