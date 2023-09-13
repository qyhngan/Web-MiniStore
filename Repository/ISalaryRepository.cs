using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ISalaryRepository
    {
        int AddSalary(Salary salary);
        int UpdateSalary(Salary salary);
        int ConfirmSalary(int salaryId);
        int CancelSalary(int salaryId);
        IEnumerable<Salary> GetSalaries();
        Salary GetSalariesByEmpIdAndSalaryUpdateDate(int empId, DateTime salaryUpdateDate);
        Salary GetSalaryByEmpIdAndDate(int empId, DateTime date);
    }
}
