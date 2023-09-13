using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Bonu
    {
        public Bonu()
        {
            EmployeeBonus = new HashSet<EmployeeBonu>();
        }

        public int BonusId { get; set; }
        public int CriteriaId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int BonusType { get; set; }
        public int? Amount { get; set; }
        public int? Percent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TimePeriod { get; set; }

        public virtual BonusCriterion Criteria { get; set; }
        public virtual ICollection<EmployeeBonu> EmployeeBonus { get; set; }
    }
}
