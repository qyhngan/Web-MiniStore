using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Salary
    {
        public int SalaryId { get; set; }
        public int EmpId { get; set; }
        public int Amount { get; set; }
        public DateTime SalaryUpdateDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; }

        public virtual Employee Emp { get; set; }
    }
}
