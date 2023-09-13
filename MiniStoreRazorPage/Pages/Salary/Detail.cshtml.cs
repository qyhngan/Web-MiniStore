using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Implement;
using System;
using System.Linq;
using Utils;

namespace MiniStoreRazorPage.Pages.Salary
{
    public class DetailModel : PageModel
    {
        public IActionResult OnPostCalculateSalary()
        {
            int empId = Int32.Parse(Request.Form["EmpId"]);
            DateTime startDate = DateTime.Parse(Request.Form["StartDate"]);
            DateTime endDate = DateTime.Parse(Request.Form["EndDate"]);
            //Set startDate, endDate to ViewData
            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;
            WorkShiftEmployeeRepository workShiftEmployeeRepository = new WorkShiftEmployeeRepository();
            var workedShifts = workShiftEmployeeRepository.GetWorkShiftEmployeesByEmpIdAndDate(empId, startDate, endDate);
            //join workedShifts with workshifts to get salaryIndex By workedShifts.WorkShiftId
            WorkShiftRepository workShiftRepository = new WorkShiftRepository();
            var workShifts = workShiftRepository.GetWorkShifts();
            var CountedShifts = from workedShift in workedShifts
                                join workShift in workShifts on workedShift.WorkShiftId equals workShift.WorkShiftId
                                orderby workedShift.Date ascending
                                select new CountedShift
                                {
                                    StartTime = workShift.StartTime,
                                    EndTime = workShift.EndTime,
                                    Date = workShift.Date,
                                    CheckinTime = workedShift.CheckinTime,
                                    CheckoutTime = workedShift.CheckoutTime,
                                    SalaryIndex = workShift.SalaryIndex
                                };
            //If CountedShifts is empty, return error
            if (CountedShifts.Count() == 0)
            {
                ViewData["Error"] = "Không có ca làm việc nào trong khoảng thời gian này";
                return Page();
            }
            else
            {
                //Store in ViewData
                ViewData["CountedShifts"] = CountedShifts;
                return Page();
            }
        }
    }
}
