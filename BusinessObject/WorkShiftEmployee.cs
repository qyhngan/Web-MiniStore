using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class WorkShiftEmployee
    {
        public int WsempId { get; set; }
        public int WorkShiftId { get; set; }
        public int EmpId { get; set; }
        public int StaffNum { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? CheckinTime { get; set; }
        public DateTime? CheckoutTime { get; set; }
        public int? Status { get; set; }
        public DateTime? Date { get; set; }

        public virtual Employee Emp { get; set; }
        public virtual WorkShift WorkShift { get; set; }
    }
}
