using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Barcode
    {
        public int BarcodeId { get; set; }
        public int ProductId { get; set; }
        public int Status { get; set; }
        public DateTime ImportedDate { get; set; }
        public DateTime SoldDate { get; set; }
        public DateTime ExpiredDate { get; set; }

        public virtual Product Product { get; set; }
    }
}
