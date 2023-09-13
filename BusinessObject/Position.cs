using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Position
    {
        public Position()
        {
            Employees = new HashSet<Employee>();
            WorkShifts = new HashSet<WorkShift>();
            Worksheets = new HashSet<Worksheet>();
        }

        public int PosId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<WorkShift> WorkShifts { get; set; }
        public virtual ICollection<Worksheet> Worksheets { get; set; }
    }
}
