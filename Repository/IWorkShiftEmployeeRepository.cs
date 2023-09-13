using BusinessObject;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IWorkShiftEmployeeRepository
    {
        int AssignWorkShiftEmployee(int WsempId, int empId, int currentEmp);
        IEnumerable<WorkShiftEmployee> GetWorkShiftEmployees();
        int UpdateLogWorkWorkShiftEmployee(int wsempId, int typeButton); //0 checkin, 1 checkout

        public IEnumerable<WorkShiftEmployee> GetWorkShiftEmployeesByEmpIdAndDate(int empId, DateTime startDate, DateTime endDate);
        void UpdateWorkShiftEmployee(WorkShiftEmployee workShiftEmployee);
        //int UpdateWorkShiftEmployee(WorkShiftEmployee workShiftEmployee);
    }
}
