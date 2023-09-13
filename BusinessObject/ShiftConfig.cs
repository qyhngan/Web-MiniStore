using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class ShiftConfig
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double SalaryIndex { get; set; }
        public int PositionId { get; set; }
        public int NumOfEmp { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public virtual Position Position { get; set; }
    }
}
