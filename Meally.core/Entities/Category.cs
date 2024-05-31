using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meally.core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public DateTime CreationDate { get; set; } 

        public Category()
        {
            CreationDate = DateTime.UtcNow;
        }
    }
}
