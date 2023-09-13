using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Voucher
    {
        public Voucher()
        {
            VoucherUseds = new HashSet<VoucherUsed>();
        }

        public int VoucherId { get; set; }
        public int Type { get; set; }
        public int ProductToApply { get; set; }
        public int CategoryToApply { get; set; }
        public int BrandToApply { get; set; }
        public int DiscountType { get; set; }
        public int DiscountAmount { get; set; }
        public int DiscountPercent { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime StartDate { get; set; }
        public int UsageLimit { get; set; }
        public int UsageCount { get; set; }
        public int MinimunAmountToApply { get; set; }
        public int QuantityToApply { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public virtual Brand BrandToApplyNavigation { get; set; }
        public virtual Category CategoryToApplyNavigation { get; set; }
        public virtual Product ProductToApplyNavigation { get; set; }
        public virtual ICollection<VoucherUsed> VoucherUseds { get; set; }
    }
}
