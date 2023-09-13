using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Implement;
using Repository;
using System.Collections;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System;
using BusinessObject;

namespace MiniStoreRazorPage.Pages.ManageWorkShiftEmployee
{

    [Authorize(Roles = "Sales, Guard")]
    public class FutureWorkShiftModel : PageModel
    {

        IWorkShiftEmployeeRepository _workShiftEmployeeRepository = new WorkShiftEmployeeRepository();
        IWorkShiftRepository _workShiftRepository = new WorkShiftRepository();
        IWorkSheetRepository _workSheetRepository = new WorkSheetRepository();
        IPositionRepository _positionRepository = new PositionRepository();
        IEmployeeRepository _employeeRepository = new EmployeeRepository();
        IRequestRepository _requestRepository = new RequestRepository();


        [BindProperty]
        public IList<WorkShiftEmployeeDisplay> WorkShiftEmployeee { get; set; }

        public void OnGet()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            string employeeId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var wsDisplay = from wse in _workShiftEmployeeRepository.GetWorkShiftEmployees()
                            join ws in _workShiftRepository.GetWorkShifts() on wse.WorkShiftId equals ws.WorkShiftId
                            join p in _positionRepository.GetPositions() on ws.PositionId equals p.PosId
                            join wsh in _workSheetRepository.GetWorkSheets() on new { ws.Date, ws.PositionId } equals new { wsh.Date, wsh.PositionId }
                            where wse.EmpId.ToString() == employeeId && (ws.Date > DateTime.Now.Date || (ws.Date == DateTime.Now.Date && ws.StartTime >= DateTime.Now.TimeOfDay))
                            orderby ws.Date ascending, ws.StartTime ascending
                            select new
                            {
                                ws.Date,
                                ws.StartTime,
                                ws.EndTime,
                                ws.NumOfEmp,
                                wsh.IsWeekend,
                                wsh.IsHoliday,
                                ws.WorkShiftId,
                                wse.WsempId
                            };

            if (wsDisplay.Count() > 0)
            {
                WorkShiftEmployeee = new List<WorkShiftEmployeeDisplay>();

                foreach (var wsd in wsDisplay)
                {
                    WorkShiftEmployeeDisplay wseDisplay = new WorkShiftEmployeeDisplay();
                    if (wsd.StartTime == new TimeSpan(18,0,0))
                    {
                        var wse = _workShiftEmployeeRepository.GetWorkShiftEmployeesByEmpIdAndDate(int.Parse(employeeId), wsd.Date.Add(wsd.StartTime), wsd.Date.AddDays(1).Add(wsd.EndTime));

                    }

                    wseDisplay.WorkShiftId = wsd.WorkShiftId;
                    wseDisplay.Date = wsd.Date;
                    wseDisplay.StartTime = wsd.StartTime;
                    wseDisplay.EndTime = wsd.EndTime;
                    wseDisplay.StaffNum = wsd.NumOfEmp;
                    wseDisplay.IsWeekend = wsd.IsWeekend;
                    wseDisplay.IsHoliday = wsd.IsHoliday;
                    WorkShiftEmployeee.Add(wseDisplay);
                }
            }

         
        }
    }
}
