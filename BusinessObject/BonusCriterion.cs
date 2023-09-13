using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class BonusCriterion
    {
        public BonusCriterion()
        {
            Bonus = new HashSet<Bonu>();
        }

        public int CriteriaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FunctionName { get; set; }
        public int Status { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public virtual ICollection<Bonu> Bonus { get; set; }
    }
}
