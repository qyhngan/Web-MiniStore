using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repository;
using Repository.Implement;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MiniStoreRazorPage.Pages.ManageWorkShiftEmployee
{
    [Authorize(Roles = "Manager, Sales, Guard")]
    public class IndexModel : PageModel
    {
        IWorkShiftEmployeeRepository _workShiftEmployeeRepository = new WorkShiftEmployeeRepository();
        IWorkShiftRepository _workShiftRepository = new WorkShiftRepository();
        IWorkSheetRepository _workSheetRepository = new WorkSheetRepository();
        IPositionRepository _positionRepository = new PositionRepository();
        IEmployeeRepository _employeeRepository = new EmployeeRepository();

        [BindProperty]
        public IList<WorkShiftEmployeeDisplay> WorkShiftEmployee { get;set; }

        public string RoleCurrentUser { get; set; }


        public void OnGet()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            string role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;
            RoleCurrentUser = role;

            //get date, position name, emp name - emp id, start time, end time, wse.modified,
            //doi lich truoc 3h
            var wsDisplay = from wse in _workShiftEmployeeRepository.GetWorkShiftEmployees()
                            join ws in _workShiftRepository.GetWorkShifts() on wse.WorkShiftId equals ws.WorkShiftId
                            join p in _positionRepository.GetPositions() on ws.PositionId equals p.PosId
                            join wsh in _workSheetRepository.GetWorkSheets() on new { ws.Date, ws.PositionId } equals new { wsh.Date, wsh.PositionId }
                            where ws.Date > DateTime.Now.Date || (ws.Date == DateTime.Now.Date && ws.StartTime >= DateTime.Now.TimeOfDay.Add((new TimeSpan(3, 0, 0)))) 
                            group wse by new { ws.Date, ws.StartTime, ws.EndTime, ws.WorkShiftId, p.Name, wsh.IsWeekend, wsh.IsHoliday, ws.NumOfEmp, p.PosId} into g
                            orderby g.Key.Date, g.Key.StartTime, g.Key.PosId
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
                                g.Key.PosId
                            };

            if (role == "Sales")
            {
                wsDisplay = wsDisplay.Where(x => x.PosId == 2);
            }
            else if (role == "Guard")
            {
                wsDisplay = wsDisplay.Where(x => x.PosId == 3);
            }

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

                    var emp = _workShiftEmployeeRepository.GetWorkShiftEmployees().Where(x => x.WorkShiftId == wsd.WorkShiftId).Select(x => x.EmpId);
                    if (emp.Count() > 0)
                    {
                        bool isUnassignAll = true;
                        foreach (var e in emp)
                        {
                            if (e != 0)
                            {
                                isUnassignAll = false;
                                break;
                            }
                        }
                        if (!isUnassignAll)
                        {
                            foreach (var e in emp)
                            {
                                if (e != 0)
                                {
                                    var empList = _employeeRepository.GetEmployees().Where(x => x.EmpId == e).Select(x => new { x.FullName, x.EmpId }).First();
                                    wseDisplay.EmpName += empList.FullName + " - " + empList.EmpId + " | ";

                                }
                                else
                                {
                                    wseDisplay.EmpName += "";
                                }
                            }

                        } 
                        else
                        {
                            wseDisplay.EmpName = "Chưa phân công";
                        }
                    }
                    WorkShiftEmployee.Add(wseDisplay);
                }
            }
        }
    }

    public class WorkShiftEmployeeDisplay
    {
        public int WorkShiftId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Ngày diễn ra")]
        public DateTime Date { get; set; }

        [DisplayName("Giờ bắt đầu")]
        public TimeSpan StartTime { get; set; }

        [DisplayName("Giờ kết thúc")]
        public TimeSpan EndTime { get; set; }

        [DisplayName("Chức vụ")]
        public string PositionName { get; set; }

        [DisplayName("Số lượng nhân viên")]
        public int StaffNum { get; set; }

        [DisplayName("Cuối tuần")]
        public bool IsWeekend { get; set; }

        [DisplayName("Lễ")]
        public bool IsHoliday { get; set; }

        [DisplayName("Tên nhân viên")]
        public string EmpName { get; set; }

        public List<string> cboEmp { get; set; }
        
        [DisplayName("Trạng thái")]
        public string Status { get; set;}

        [DisplayName("Trạng thái điểm danh")]
        public string LogworkStatus { get; set; }

        [DisplayName("Giờ điểm danh")]
        public DateTime? Checkin { get; set; }

        [DisplayName("Giờ tan ca")]
        public DateTime? Checkout { get; set; }
        public int? statusRequest { get; set; }

    }
}
