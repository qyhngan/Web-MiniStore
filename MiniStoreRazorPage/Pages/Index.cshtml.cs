using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStoreRazorPage.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public byte ButtonLogWork { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        IWorkShiftEmployeeRepository _workShiftEmployeeRepository = new WorkShiftEmployeeRepository();
        IWorkShiftRepository _workShiftRepository = new WorkShiftRepository();
        IEmployeeRepository _employeeRepository = new EmployeeRepository();

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/");
            }
            HttpContext.Session.SetInt32("ButtonLogWork", ShowLogWorkButton());
            ButtonLogWork = ShowLogWorkButton();
            return Page();
        }

        // query wsf_emp joim wsf => (empid = user.empid, date = current date, checkintime = null, starttime - 15 < current time < endtime) => hien nut check in
        // truoc 15p thi nut check in se active
        private byte ShowLogWorkButton()
        {
            var username = User.Identity.Name;
            var empId = _employeeRepository.GetEmployees().Where(x => x.UserName == username).Select(x => x.EmpId).First();
            var workShiftEmployee = _workShiftEmployeeRepository.GetWorkShiftEmployees();
            var workShift = _workShiftRepository.GetWorkShifts();
            var join = from wse in workShiftEmployee
                       join ws in workShift on wse.WorkShiftId equals ws.WorkShiftId
                       where wse.EmpId == empId && wse.CheckinTime == null && 
                             ((ws.StartTime < ws.EndTime && ws.Date == DateTime.Now.Date && ws.StartTime - TimeSpan.FromMinutes(15) < DateTime.Now.TimeOfDay && DateTime.Now.TimeOfDay < ws.EndTime)||
                              (ws.StartTime > ws.EndTime && ((ws.Date == DateTime.Now.Date && ws.StartTime - TimeSpan.FromMinutes(15) < DateTime.Now.TimeOfDay) || 
                                                             (ws.Date == DateTime.Now.Date.AddDays(-1) && DateTime.Now.TimeOfDay < ws.EndTime))))
                       select wse;
            if (join.Count() > 0)
            {
                HttpContext.Session.SetInt32("WorkShiftEmployeeId", join.Select(x => x.WsempId).First());
                return 0; // check in button
            }
            else
            {
                join = from wse in workShiftEmployee
                       join ws in workShift on wse.WorkShiftId equals ws.WorkShiftId
                       where wse.EmpId == empId && wse.CheckinTime != null &&
                             ((ws.StartTime < ws.EndTime && ws.Date == DateTime.Now.Date && ws.StartTime - TimeSpan.FromMinutes(15) < DateTime.Now.TimeOfDay && DateTime.Now.TimeOfDay < ws.EndTime) ||
                              (ws.StartTime > ws.EndTime && ((ws.Date == DateTime.Now.Date && ws.StartTime - TimeSpan.FromMinutes(15) < DateTime.Now.TimeOfDay) ||
                                                             (ws.Date == DateTime.Now.Date.AddDays(-1) && DateTime.Now.TimeOfDay < ws.EndTime))))
                       select wse;
                if (join.Count() > 0)
                {
                    HttpContext.Session.SetInt32("WorkShiftEmployeeId", join.Select(x => x.WsempId).First());
                    return 1; // check out button
                }
                else
                {
                    HttpContext.Session.SetInt32("WorkShiftEmployeeId", -1);
                    return 2; // nothing
                }
            }
        }
    }
}
