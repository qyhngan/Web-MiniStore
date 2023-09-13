using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class WorkShift
    {
        public WorkShift()
        {
            WorkShiftEmployees = new HashSet<WorkShiftEmployee>();
        }

        public int WorkShiftId { get; set; }
        public DateTime Date { get; set; }
        public int PositionId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int NumOfEmp { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public double? SalaryIndex { get; set; }
        public int? Status { get; set; }

        public virtual Position Position { get; set; }
        public virtual Worksheet Worksheet { get; set; }
        public virtual ICollection<WorkShiftEmployee> WorkShiftEmployees { get; set; }
    }
}
