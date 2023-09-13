using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IEmployeeRepository
    {
        int DeleteEmployee(int v);
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeById(int empId);
        int AddEmployee(Employee employee);
        int UpdateEmployee(Employee employee);
        Employee GetEmployee(string username, string password);
    }
}
