using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class SalaryReportRepository : ISalaryReportRepository
    {
        public IEnumerable<SalaryReport> GenerateSalaryReport(DateTime period) => SalaryReportDAO.Instance.GenerateSalaryReport(period);

        public IEnumerable<SalaryReport> GetReports() => SalaryReportDAO.Instance.GetReports();

        public IEnumerable<SalaryReport> GetReportsByPeriod(DateTime period) => SalaryReportDAO.Instance.GetReportsByPeriod(period);
    }
}
