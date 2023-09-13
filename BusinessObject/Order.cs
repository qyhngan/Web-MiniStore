using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Shipments = new HashSet<Shipment>();
            VoucherUseds = new HashSet<VoucherUsed>();
        }

        public int OrderId { get; set; }
        public int Saler { get; set; }
        public int Customer { get; set; }
        public int TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
        public int TotalTaxIncluded { get; set; }
        public int PaymentMethod { get; set; }
        public int CounterNo { get; set; }
        public int Type { get; set; }
        public int CustomerGive { get; set; }
        public int Payback { get; set; }
        public int TotalDiscount { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual Customer CustomerNavigation { get; set; }
        public virtual Employee SalerNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
        public virtual ICollection<VoucherUsed> VoucherUseds { get; set; }
    }
}
