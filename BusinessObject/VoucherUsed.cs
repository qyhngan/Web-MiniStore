using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class VoucherUsed
    {
        public int UsedId { get; set; }
        public int CustomerId { get; set; }
        public int VoucherId { get; set; }
        public int OrderId { get; set; }
        public DateTime UsedDate { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Order Order { get; set; }
        public virtual Voucher Voucher { get; set; }
    }
}
