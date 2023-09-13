using Microsoft.AspNetCore.Authorization;
using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Implement;
using System;
using System.Linq;
using Utils;
using Repository;

namespace MiniStoreRazorPage.Pages.Salary
{
    [Authorize(Roles = "Manager")]
    public class ExportModel : PageModel
    {
        public IActionResult OnPostCalculateSalary()
        {
            DateTime period = DateTime.Parse(Request.Form["Period"]);
            ViewData["Period"] = period.ToString("yyyy-MM");
            //Get last date of month From period
            DateTime lastDateOfMonth = new DateTime(period.Year, period.Month, DateTime.DaysInMonth(period.Year, period.Month));

            //Check if in SalaryReport table has this month
            ISalaryReportRepository salaryReportRepository = new SalaryReportRepository();
            var salaryReports = salaryReportRepository.GenerateSalaryReport(lastDateOfMonth);
            if (salaryReports.Count() == 0)
            {
                ViewData["Error"] = "Không báo cáo nào được tạo";
                return Page();
            }
            else
            {
                //Store in ViewData
                ViewData["SalaryReports"] = salaryReports;
                return Page();
            }
        }
    }
}
