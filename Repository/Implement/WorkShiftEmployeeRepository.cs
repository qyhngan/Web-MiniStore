using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class WorkShiftEmployeeRepository : IWorkShiftEmployeeRepository
    {
        public int AssignWorkShiftEmployee(int WsempId, int empId, int currentEmp) => WorkShiftEmployeeDAO.Instance.AssignWorkShiftEmployee(WsempId, empId, currentEmp);

        public IEnumerable<WorkShiftEmployee> GetWorkShiftEmployees() => WorkShiftEmployeeDAO.Instance.GetWorkShiftEmployees();

        public int UpdateLogWorkWorkShiftEmployee(int wsempId, int typeButton) => WorkShiftEmployeeDAO.Instance.UpdateLogWorkWorkShiftEmployee(wsempId, typeButton);

        public IEnumerable<WorkShiftEmployee> GetWorkShiftEmployeesByEmpIdAndDate(int empId, DateTime startDate, DateTime endDate) => WorkShiftEmployeeDAO.Instance.GetWorkShiftEmployeesByEmpIdAndDate(empId, startDate, endDate);

        public void UpdateWorkShiftEmployee(WorkShiftEmployee workShiftEmployee) => WorkShiftEmployeeDAO.Instance.UpdateWorkShiftEmployee(workShiftEmployee);

        // public bool UpdateWorkShiftEmployee(WorkShiftEmployee workShiftEmployee) => WorkShiftEmployeeDAO.Instance.UpdateWorkShiftEmployee(workShiftEmployee);
    }
}
