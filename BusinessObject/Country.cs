using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Country
    {
        public Country()
        {
            Brands = new HashSet<Brand>();
            Origins = new HashSet<Origin>();
        }

        public int CountryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Brand> Brands { get; set; }
        public virtual ICollection<Origin> Origins { get; set; }
    }
}
