using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Product
    {
        public Product()
        {
            Barcodes = new HashSet<Barcode>();
            OrderDetails = new HashSet<OrderDetail>();
            Vouchers = new HashSet<Voucher>();
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int OriginId { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int Tax { get; set; }
        public int UnitType { get; set; }
        public int DiscountAmount { get; set; }
        public int InStock { get; set; }
        public int SaleCount { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string ImgUrl { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual Location Location { get; set; }
        public virtual Origin Origin { get; set; }
        public virtual ICollection<Barcode> Barcodes { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
