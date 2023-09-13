using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Location
    {
        public Location()
        {
            Products = new HashSet<Product>();
        }

        public int LocationId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
