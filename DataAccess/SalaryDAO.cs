using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SalaryDAO
    {
        // Design Pattern: Singleton
        private static SalaryDAO instance = null;
        private static readonly object instanceLock = new object();
        private SalaryDAO() { }
        public static SalaryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SalaryDAO();
                    }
                    return instance;
                }
            }
        }
        public int AddSalary(Salary salary)
        {
            int status = 0;
            try
            {
                var dbContext = new MiniStoreContext();
                dbContext.Salaries.Add(salary);
                status = dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return status;
        }
        public int UpdateSalary(Salary salary)
        {
            int status = 0;
            try
            {
                var dbContext = new MiniStoreContext();
                Salary salaryUpdate = dbContext.Salaries.Where(x => x.SalaryId == salary.SalaryId).FirstOrDefault();
                if (salaryUpdate != null)
                {
                    salaryUpdate.Amount = salary.Amount;
                    salaryUpdate.StartDate = salary.StartDate;
                    salaryUpdate.EndDate = salary.EndDate;
                    salaryUpdate.ModifiedTime = salary.ModifiedTime;
                    salaryUpdate.ModifiedBy = salary.ModifiedBy;
                    salaryUpdate.Status = salary.Status;
                    status = dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return status;
        }
        public int CancelSalary(int salaryId)
        {
            int status = 0;
            try
            {
                var dbContext = new MiniStoreContext();
                Salary salaryUpdate = dbContext.Salaries.Where(x => x.SalaryId == salaryId).FirstOrDefault();
                if (salaryUpdate != null)
                {
                    salaryUpdate.Status = 5;
                    status = dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            return status;
        }

        public int ConfirmSalary(int salaryId)
        {
            int status = 0;
            try
            {
                var dbContext = new MiniStoreContext();
                Salary salaryUpdate = dbContext.Salaries.Where(x => x.SalaryId == salaryId).FirstOrDefault();
                if (salaryUpdate != null)
                {
                    salaryUpdate.Status = 2;
                    status = dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return status;
        }

        public IEnumerable<Salary> GetSalaries()
        {
            IEnumerable<Salary> salaries = null;
            try
            {
                var dbContext = new MiniStoreContext();
                salaries = dbContext.Salaries.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return salaries;
        }
        public Salary GetSalariesByEmpIdAndSalaryUpdateDate(int empId, DateTime salaryUpdateDate)
        {
            Salary salary = null;
            try
            {
                var dbContext = new MiniStoreContext();
                salary = dbContext.Salaries.Where(x => x.EmpId == empId && x.SalaryUpdateDate == salaryUpdateDate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return salary;
        }

        public Salary GetSalaryByEmpIdAndDate(int empId, DateTime date)
        {
            //Get Salary by EmpId and Salary.StartDate <= date <= Salary.EndDate
            Salary salary = null;
            try
            {
                var dbContext = new MiniStoreContext();
                salary = dbContext.Salaries.Where(x => x.EmpId == empId && x.StartDate <= date && x.EndDate >= date && (x.Status == 3 || x.Status == 4)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return salary;
        }
    }
}
