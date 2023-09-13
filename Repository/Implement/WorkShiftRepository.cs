using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class WorkShiftRepository : IWorkShiftRepository
    {
        public IEnumerable<WorkShift> GetWorkShifts() => WorkShiftDAO.Instance.GetWorkShifts();
    }
}
