using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
            VoucherUseds = new HashSet<VoucherUsed>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime MemberSince { get; set; }
        public int Level { get; set; }
        public int AccumulatePoint { get; set; }
        public int TotalSpend { get; set; }
        public string Address { get; set; }
        public DateTime LastBuyDate { get; set; }
        public int Status { get; set; }
        public int TotalOrder { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<VoucherUsed> VoucherUseds { get; set; }
    }
}
