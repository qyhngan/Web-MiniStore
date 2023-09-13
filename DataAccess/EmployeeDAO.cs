using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class EmployeeDAO
    {
        private static EmployeeDAO instance = null;
        private static readonly object instanceLock = new object();

        public static EmployeeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new EmployeeDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees;
            try
            {
                var dbContext = new MiniStoreContext();
                employees = dbContext.Employees.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return employees;
        }

        public int DeleteEmployee(int empId)
        {
            //Delete Employee by empId
            int status = 0;
            try
            {
                var dbContext = new MiniStoreContext();
                Employee employee = dbContext.Employees.Where(x => x.EmpId == empId).FirstOrDefault();
                if (employee != null)
                {
                    dbContext.Employees.Remove(employee);
                    status = dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return status;
        }

        public int AddEmployee(Employee employee)
        {
            //add new Employee
            int status = 0;
            try
            {
                var dbContext = new MiniStoreContext();
                dbContext.Employees.Add(employee);
                status = dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return status;
        }

        public int UpdateEmployee(Employee employee)
        {
            //update Employee
            int status = 0;
            try
            {
                var dbContext = new MiniStoreContext();
                Employee emp = dbContext.Employees.Where(x => x.EmpId == employee.EmpId).FirstOrDefault();
                if (emp != null)
                {
                    //all field of Employee
                    emp.FullName = employee.FullName;
                    emp.Birthday = employee.Birthday;
                    emp.FirstDateAtWork = employee.FirstDateAtWork;
                    emp.Image = employee.Image;
                    emp.Salary = employee.Salary;
                    emp.PhoneNumber = employee.PhoneNumber;
                    emp.SalaryUpdateDate = employee.SalaryUpdateDate;
                    emp.Address = employee.Address;
                    emp.Status = employee.Status;
                    emp.UserName = employee.UserName;
                    emp.Password = employee.Password;
                    emp.ModifiedBy = employee.ModifiedBy;
                    emp.ModifiedDate = employee.ModifiedDate;
                    emp.PosId = employee.PosId;
                    status = dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return status;
        }

        public Employee GetEmployeeById(int empId)
        {
            Employee employee;
            try
            {
                var dbContext = new MiniStoreContext();
                employee = dbContext.Employees.Where(x => x.EmpId == empId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return employee;
        }

        public Employee Employee(string username, string password)
        {
            Employee employee;
            try
            {
                var dbContext = new MiniStoreContext();
                employee = dbContext.Employees.Where(x => x.UserName == username).FirstOrDefault();

                if (employee != null)
                {
                    string hash = employee.Password;
                    if (BCrypt.Net.BCrypt.Verify(username + password, hash))
                    {
                        return employee;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
