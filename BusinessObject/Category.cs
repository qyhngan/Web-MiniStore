using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
            Vouchers = new HashSet<Voucher>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Tax { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
