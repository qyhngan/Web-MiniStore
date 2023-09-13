using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Worksheet
    {
        public Worksheet()
        {
            WorkShifts = new HashSet<WorkShift>();
        }

        public int WorksheetId { get; set; }
        public DateTime Date { get; set; }
        public bool IsHoliday { get; set; }
        public bool IsWeekend { get; set; }
        public bool IsConfirm { get; set; }
        public int PositionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public double? SalaryIndex { get; set; }
        public int? Status { get; set; }

        public virtual Position Position { get; set; }
        public virtual ICollection<WorkShift> WorkShifts { get; set; }
    }
}
