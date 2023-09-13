using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SalaryReportDAO
    {
        //singleton
        private static SalaryReportDAO instance = null;
        private static readonly object instanceLock = new object();
        public static SalaryReportDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SalaryReportDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<SalaryReport> GetReports()
        {
            //Get all SalaryReport
            IEnumerable<SalaryReport> salaryReports;
            try
            {
                var dbContext = new MiniStoreContext();
                salaryReports = dbContext.SalaryReports.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return salaryReports;
        }

        public IEnumerable<SalaryReport> GetReportsByPeriod(DateTime period)
        {
            //Get SalaryReport by period
            IEnumerable<SalaryReport> salaryReports;
            try
            {
                var dbContext = new MiniStoreContext();
                salaryReports = dbContext.SalaryReports.Where(x => x.Period == period).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return salaryReports;
        }

        public IEnumerable<SalaryReport> GenerateSalaryReport(DateTime period)
        {
            
            IEnumerable<SalaryReport> salaryReports;
            try
            {
                var dbContext = new MiniStoreContext();
                //Delete all SalaryReport with period
                dbContext.Database.ExecuteSqlRaw("DELETE FROM SalaryReport WHERE [Period] = {0}", period);
                //Call Store Procedure to dbo.GenerateSalaryReport(period)
                dbContext.Database.ExecuteSqlRaw("dbo.GenerateSalaryReport {0}", period);
                salaryReports = dbContext.SalaryReports.Where(x => x.Period == period).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return salaryReports;

        }
    }
}
