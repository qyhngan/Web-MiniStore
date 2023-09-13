using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class SalaryReport
    {
        public int ReportId { get; set; }
        public int EmpId { get; set; }
        public int BaseSalary { get; set; }
        public int TotalBonus { get; set; }
        public DateTime Period { get; set; }
        public int Status { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int? Total { get; set; }

        public virtual Employee Emp { get; set; }
    }
}
