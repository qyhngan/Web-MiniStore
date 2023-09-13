using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class SalaryRepository : ISalaryRepository
    {
        public int AddSalary(Salary salary) => SalaryDAO.Instance.AddSalary(salary);

        public int CancelSalary(int salaryId) => SalaryDAO.Instance.CancelSalary(salaryId);

        public int ConfirmSalary(int salaryId) => SalaryDAO.Instance.ConfirmSalary(salaryId);

        public IEnumerable<Salary> GetSalaries() => SalaryDAO.Instance.GetSalaries();

        public Salary GetSalariesByEmpIdAndSalaryUpdateDate(int empId, DateTime salaryUpdateDate) => SalaryDAO.Instance.GetSalariesByEmpIdAndSalaryUpdateDate(empId, salaryUpdateDate);

        public Salary GetSalaryByEmpIdAndDate(int empId, DateTime date) => SalaryDAO.Instance.GetSalaryByEmpIdAndDate(empId, date);

        public int UpdateSalary(Salary salary) => SalaryDAO.Instance.UpdateSalary(salary);
    }
}
