using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Origin
    {
        public Origin()
        {
            Products = new HashSet<Product>();
        }

        public int OriginId { get; set; }
        public int CountryId { get; set; }
        public string Province { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
