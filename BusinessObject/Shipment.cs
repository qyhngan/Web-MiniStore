using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Shipment
    {
        public int ShipmentId { get; set; }
        public int OrderId { get; set; }
        public int Status { get; set; }
        public DateTime ShippingTime { get; set; }
        public DateTime EstimateShippedTime { get; set; }
        public int DeliveryType { get; set; }
        public int DeliveryUnit { get; set; }
        public DateTime DeliveriedTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public virtual Order Order { get; set; }
    }
}
