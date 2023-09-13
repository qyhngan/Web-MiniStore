using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class EmployeeBonu
    {
        public int QualifyId { get; set; }
        public int EmpId { get; set; }
        public int BonusId { get; set; }
        public bool Qualified { get; set; }
        public DateTime Period { get; set; }
        public int Status { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public virtual Bonu Bonus { get; set; }
        public virtual Employee Emp { get; set; }
    }
}
