using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Brand
    {
        public Brand()
        {
            Products = new HashSet<Product>();
            Vouchers = new HashSet<Voucher>();
        }

        public int BrandId { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string Description { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
