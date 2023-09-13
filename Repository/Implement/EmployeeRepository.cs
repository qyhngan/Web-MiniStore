using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;

namespace Repository.Implement
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public int AddEmployee(Employee employee) => EmployeeDAO.Instance.AddEmployee(employee);

        public int DeleteEmployee(int empId) => EmployeeDAO.Instance.DeleteEmployee(empId);
        public IEnumerable<Employee> GetEmployees() => EmployeeDAO.Instance.GetEmployees();
        public Employee GetEmployeeById(int empId) => EmployeeDAO.Instance.GetEmployeeById(empId);

        public int UpdateEmployee(Employee employee) => EmployeeDAO.Instance.UpdateEmployee(employee);

        public Employee GetEmployee(string username, string password) => EmployeeDAO.Instance.Employee(username, password);
    }
}
