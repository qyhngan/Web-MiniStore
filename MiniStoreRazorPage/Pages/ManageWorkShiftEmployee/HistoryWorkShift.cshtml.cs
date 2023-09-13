using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repository.Implement;
using Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MiniStoreRazorPage.Pages.ManageWorkShiftEmployee
{
    [Authorize(Roles = "Manager, Sales, Guard")]
    public class HistoryWorkShiftModel : PageModel
    {
        IWorkShiftEmployeeRepository _workShiftEmployeeRepository = new WorkShiftEmployeeRepository();
        IWorkShiftRepository _workShiftRepository = new WorkShiftRepository();
        IWorkSheetRepository _workSheetRepository = new WorkSheetRepository();
        IPositionRepository _positionRepository = new PositionRepository();
        IEmployeeRepository _employeeRepository = new EmployeeRepository();

        [BindProperty]
        public IList<WorkShiftEmployeeDisplay> WorkShiftEmployee { get; set; }

        public string RoleCurrentUser { get; set; }

        public void OnGet()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            string role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;
            RoleCurrentUser = role;
            string empId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            

            if (role == "Sales" || role == "Guard")
            {
                var wsDisplay = from wse in _workShiftEmployeeRepository.GetWorkShiftEmployees()
                                join ws in _workShiftRepository.GetWorkShifts() on wse.WorkShiftId equals ws.WorkShiftId
                                join p in _positionRepository.GetPositions() on ws.PositionId equals p.PosId
                                join wsh in _workSheetRepository.GetWorkSheets() on new { ws.Date, ws.PositionId } equals new { wsh.Date, wsh.PositionId }
                                where wse.EmpId.ToString() == empId && (ws.Date < DateTime.Now.Date || (ws.Date == DateTime.Now.Date && ws.StartTime <= DateTime.Now.TimeOfDay))
                                orderby ws.Date descending, ws.StartTime descending
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
                    WorkShiftEmployee = new List<WorkShiftEmployeeDisplay>();

                    foreach (var wsd in wsDisplay)
                    {
                        WorkShiftEmployeeDisplay wseDisplay = new WorkShiftEmployeeDisplay();
                        wseDisplay.WorkShiftId = wsd.WorkShiftId;
                        wseDisplay.Date = wsd.Date;
                        wseDisplay.StartTime = wsd.StartTime;
                        wseDisplay.EndTime = wsd.EndTime;
                        wseDisplay.StaffNum = wsd.NumOfEmp;
                        wseDisplay.IsWeekend = wsd.IsWeekend;
                        wseDisplay.IsHoliday = wsd.IsHoliday;


                        var emp = _workShiftEmployeeRepository.GetWorkShiftEmployees().Where(x => x.WsempId == wsd.WsempId).Select(x => new { x.CheckinTime, x.CheckoutTime }).First();
                        wseDisplay.Checkin = emp.CheckinTime;
                        wseDisplay.Checkout = emp.CheckoutTime;

                        if (wsd.Date < DateTime.Now.Date || (wsd.Date == DateTime.Now.Date && wsd.EndTime <= DateTime.Now.TimeOfDay))
                        {
                            wseDisplay.Status = "Đã kết thúc";

                            if (emp.CheckinTime == null)
                            {
                                wseDisplay.LogworkStatus = "Vắng";
                            }
                            else
                            {
                                wseDisplay.LogworkStatus = "Đã hoàn thành";
                            }
                        }
                        else
                        {
                            wseDisplay.Status = "Đang diễn ra";
                            
                            if (emp.CheckinTime == null)
                            {
                                wseDisplay.LogworkStatus = "Chưa điểm danh";
                            }
                            else if (emp.CheckoutTime == null)
                            {
                                wseDisplay.LogworkStatus = "Đang làm việc";
                            }
                            else
                            {
                                wseDisplay.LogworkStatus = "Đã hoàn thành";
                            }
                        }


                        WorkShiftEmployee.Add(wseDisplay);
                    }
                }
            }
            else if (role == "Manager")
            {
                var wsDisplay = from wse in _workShiftEmployeeRepository.GetWorkShiftEmployees()
                                join ws in _workShiftRepository.GetWorkShifts() on wse.WorkShiftId equals ws.WorkShiftId
                                join p in _positionRepository.GetPositions() on ws.PositionId equals p.PosId
                                join wsh in _workSheetRepository.GetWorkSheets() on new { ws.Date, ws.PositionId } equals new { wsh.Date, wsh.PositionId }
                                where ws.Date < DateTime.Now.Date || (ws.Date == DateTime.Now.Date && ws.StartTime <= DateTime.Now.TimeOfDay)
                                group wse by new { ws.Date, ws.StartTime, ws.EndTime, ws.WorkShiftId, p.Name, wsh.IsWeekend, wsh.IsHoliday, ws.NumOfEmp, p.PosId } into g
                                orderby g.Key.Date descending, g.Key.StartTime descending, g.Key.PosId
                                select new
                                {
                                    g.Key.Date,
                                    g.Key.StartTime,
                                    g.Key.EndTime,
                                    g.Key.Name,
                                    g.Key.NumOfEmp,
                                    g.Key.IsWeekend,
                                    g.Key.IsHoliday,
                                    g.Key.WorkShiftId,
                                    g.Key.PosId,
                                };

                if (wsDisplay.Count() > 0)
                {
                    WorkShiftEmployee = new List<WorkShiftEmployeeDisplay>();

                    foreach (var wsd in wsDisplay)
                    {
                        WorkShiftEmployeeDisplay wseDisplay = new WorkShiftEmployeeDisplay();
                        wseDisplay.WorkShiftId = wsd.WorkShiftId;
                        wseDisplay.Date = wsd.Date;
                        wseDisplay.StartTime = wsd.StartTime;
                        wseDisplay.EndTime = wsd.EndTime;
                        wseDisplay.PositionName = wsd.Name;
                        wseDisplay.StaffNum = wsd.NumOfEmp;
                        wseDisplay.IsWeekend = wsd.IsWeekend;
                        wseDisplay.IsHoliday = wsd.IsHoliday;


                        if (wsd.Date < DateTime.Now.Date || (wsd.Date == DateTime.Now.Date && wsd.EndTime <= DateTime.Now.TimeOfDay))
                        {
                            wseDisplay.Status = "Đã kết thúc";
                        }
                        else
                        {
                            wseDisplay.Status = "Đang diễn ra";
                        }

                        var emp = _workShiftEmployeeRepository.GetWorkShiftEmployees().Where(x => x.WorkShiftId == wsd.WorkShiftId).Select(x => new { x.EmpId, x.CheckinTime, x.CheckoutTime });

                        if (emp.Count() > 0)
                        {
                            bool isUnassignAll = true;
                            foreach (var e in emp)
                            {
                                if (e.EmpId != 0)
                                {
                                    isUnassignAll = false;
                                    break;
                                }
                            }
                            if (!isUnassignAll)
                            {
                                bool allAbsent = true;
                                foreach (var e in emp)
                                {
                                    if (e.EmpId != 0)
                                    {
                                        var empList = _employeeRepository.GetEmployees().Where(x => x.EmpId == e.EmpId).Select(x => new { x.FullName, x.EmpId }).First();
                                        wseDisplay.EmpName += empList.FullName + " - " + empList.EmpId;
                                        if (wseDisplay.Status == "Đã kết thúc" && e.CheckinTime == null)
                                        {
                                            wseDisplay.EmpName += " (Vắng) " + " | ";
                                        }
                                        else if (e.CheckinTime != null)
                                        {
                                            wseDisplay.EmpName += " (Có mặt) " + " | ";
                                            allAbsent = false;
                                        }
                                        else if (wseDisplay.Status == "Đang diễn ra" && e.CheckinTime == null)
                                        {
                                            wseDisplay.EmpName += " (Chưa có mặt) " + " | ";
                                        }
                                    }
                                    else
                                    {
                                        wseDisplay.EmpName += "";
                                    }
                                }
                                if (allAbsent)
                                {
                                    wseDisplay.Status = "Cảnh báo";
                                }

                            }
                            else
                            {
                                wseDisplay.EmpName = "Chưa phân công";
                                wseDisplay.Status = "Cảnh báo";
                            }
                        }
                        WorkShiftEmployee.Add(wseDisplay);
                    }
                }
            }
        }
    }
}

