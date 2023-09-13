using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repository;
using Repository.Implement;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace MiniStoreRazorPage.Pages.ManageWorkShiftEmployee
{
    [Authorize(Roles = "Manager, Sales, Guard")]
    public class AssignShiftModel : PageModel
    {
        IWorkShiftEmployeeRepository _workShiftEmployeeRepository = new WorkShiftEmployeeRepository();
        IWorkShiftRepository _workShiftRepository = new WorkShiftRepository();
        IEmployeeRepository _employeeRepository = new EmployeeRepository();
         
        [BindProperty]
        public List<WorkShiftShowed> WorkShiftEmployee { get; set; } 
        public string RoleCurrentUser { get; set; }

        public IActionResult OnGet(int? id)
        {

            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            string role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;
            RoleCurrentUser = role;

            if (id == null)
            {
                return NotFound();
            }

            var wseList = _workShiftEmployeeRepository.GetWorkShiftEmployees().Where(x => x.WorkShiftId == id).ToList();
            var workShift = _workShiftRepository.GetWorkShifts().Where(x => x.WorkShiftId == id).FirstOrDefault();
            int posId = workShift.PositionId;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
            var empCbo = _employeeRepository.GetEmployees().Where(x => x.PosId == posId).Select(x => new { x.EmpId, V = x.FullName + " - " + x.EmpId }) ;

            if (wseList == null)
            {
                return NotFound();
            }

            WorkShiftEmployee = new List<WorkShiftShowed>();
            foreach (var wse in wseList)
            {
                WorkShiftShowed wss = new WorkShiftShowed
                {
                    WsempId = wse.WsempId,
                    WorkShiftId = wse.WorkShiftId,
                    EmpId = wse.EmpId,
                    StaffNum = wse.StaffNum,
                    ModifiedTime = wse.ModifiedTime,
                    ModifiedBy = wse.ModifiedBy,
                    Date = wse.Date,
                };
                if (wse.ModifiedBy != 0)
                {
                    wss.NameModifiedBy = _employeeRepository.GetEmployees().Where(x => x.EmpId == wse.ModifiedBy).FirstOrDefault().FullName + " - " + wse.ModifiedBy;
                } 
                else
                {
                    wss.NameModifiedBy = "Chưa có";
                }
                if (wse.EmpId != 0)
                {
                    wss.NameEmp = _employeeRepository.GetEmployees().Where(x => x.EmpId == wse.EmpId).FirstOrDefault().FullName + " - " + wse.EmpId;
                }
                else
                {
                    wss.NameEmp = "Chưa có";
                }
                WorkShiftEmployee.Add(wss);
            }

            SelectList sl = new SelectList(empCbo, "EmpId", "V");
            ViewData["EmpCbo"] = sl;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        [Authorize(Roles = "Manager")]
        public IActionResult OnPostAssign()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Index");

            }

            try
            {
                ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
                string curEmpID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int wsid = int.Parse(Request.Form["wsid"]);
                int staffNo = int.Parse(Request.Form["staffNo"]);
                int inputWseId = int.Parse(Request.Form["wsempid"]);
                string inputEmpId = Request.Form["selectedValue"];
                if (!string.IsNullOrEmpty(curEmpID))
                {
                    if (_workShiftEmployeeRepository.GetWorkShiftEmployees().Where(x => x.WorkShiftId == wsid && x.EmpId == int.Parse(inputEmpId) && x.StaffNum != staffNo).Count() > 0)
                    {
                        ViewData["Message"] = "Nhân viên đã có lịch phân công";
                        return RedirectToPage("./Index");

                    }
                    int status = _workShiftEmployeeRepository.AssignWorkShiftEmployee(inputWseId, int.Parse(inputEmpId), int.Parse(curEmpID));
                    if (status > 0)
                    {
                        ViewData["Message"] = "Phân việc thành công";
                        return RedirectToPage("./Index");
                    }
                    else
                    {
                        ViewData["Message"] = "Phân việc thất bại";
                        return RedirectToPage("./Index");
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkShiftEmployeeExists(int.Parse(Request.Form["wsempid"])))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        [Authorize(Roles = "Manager")]
        public IActionResult OnPostUnassign()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
                string curEmpID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(curEmpID))
                {
                    int status = _workShiftEmployeeRepository.AssignWorkShiftEmployee(int.Parse(Request.Form["wsempid"]), 0, int.Parse(curEmpID));
                    if (status > 0)
                    {
                        ViewData["Message"] = "Hủy phân công thành công";
                        return RedirectToPage("./Index");
                    }
                    else
                    {
                        ViewData["Message"] = "Hủy phân công thất bại";
                        return Page();
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkShiftEmployeeExists(int.Parse(Request.Form["wsempid"])))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        [Authorize(Roles = "Sales, Guard")]
        public IActionResult OnPostAssignMe()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
                string curEmpID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                var wse = _workShiftEmployeeRepository.GetWorkShiftEmployees().Where(x => x.WorkShiftId == int.Parse(Request.Form["wsid"]));
                bool isFull = true;
                int wseEmpty = 0;
                foreach (WorkShiftEmployee w in wse)
                {
                    if (w.EmpId == int.Parse(curEmpID))
                    {
                        isFull = true;
                        break;
                    }

                    if (w.EmpId == 0)
                    {
                        isFull = false;
                        wseEmpty = w.WsempId;
                        break;
                    }
                }

                if (isFull)
                {
                    ViewData["Message"] = "Ca đã đủ nhân viên. Không thể nhận ca này";
                    return RedirectToPage("./Index");
                }

                if (!string.IsNullOrEmpty(curEmpID))
                {
                    int status = _workShiftEmployeeRepository.AssignWorkShiftEmployee(wseEmpty, int.Parse(curEmpID), int.Parse(curEmpID));
                    if (status > 0)
                    {
                        ViewData["Message"] = "Nhận ca thành công";
                        return RedirectToPage("./Index");
                    }
                    else
                    {
                        ViewData["Message"] = "Nhận ca thất bại";
                        return RedirectToPage("./Index");

                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkShiftEmployeeExists(int.Parse(Request.Form["wsempid"])))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WorkShiftEmployeeExists(int id)
        {
            return _workShiftEmployeeRepository.GetWorkShiftEmployees().Any(e => e.WsempId == id);
        }
    }
}

public class WorkShiftShowed
{
    public int WsempId { get; set; }
    public int WorkShiftId { get; set; }
    public int EmpId { get; set; }
    [Display(Name = "Số lượng nhân viên")]
    public int StaffNum { get; set; }
    [Display(Name = "Thời gian sửa")]
    public DateTime ModifiedTime { get; set; }
    [Display(Name = "Người sửa")]
    public int ModifiedBy { get; set; }
    [Display(Name = "Ngày diễn ra")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime? Date { get; set; }
    [Display(Name = "Tên người sửa")]
    public String NameModifiedBy { get; set; }
    [Display(Name = "Tên nhân viên")]
    public String NameEmp { get; set; }
}
