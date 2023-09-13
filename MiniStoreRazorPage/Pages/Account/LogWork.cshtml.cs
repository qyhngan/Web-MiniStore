using BusinessObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.Implement;
using System;
using System.Linq;

namespace MiniStoreRazorPage.Pages.Account
{
    [Authorize]
    public class LogWorkModel : PageModel
    {
        IEmployeeRepository _employeeRepository = new EmployeeRepository();
        IWorkShiftEmployeeRepository _workShiftEmployeeRepository = new WorkShiftEmployeeRepository();
        public void OnGet()
        {
        }


        public IActionResult OnPostCheckIn(string password)
        {
            
            if (CheckPassword(password))
            {
                int status = _workShiftEmployeeRepository.UpdateLogWorkWorkShiftEmployee(GetWorkShiftEmployee().WsempId, 0); //checkin
                if (status > 0)
                {
                    TempData["Success"] = "Checkout success.";
                    ViewData["AnnounceMessage"] = "Checkin success.";
                }
                else
                {
                    ViewData["AnnounceMessage"] = "Checkin fail.";
                }
                return Page();
            }
            else
            {
                // Password is incorrect, display an error message or take appropriate action
                ViewData["AnnounceMessage"] = "Incorrect password.";
                return Page();
            }
        }

        public IActionResult OnPostCheckOut(string password)
        {
            if (CheckPassword(password))
            {
                int status = _workShiftEmployeeRepository.UpdateLogWorkWorkShiftEmployee(GetWorkShiftEmployee().WsempId, 1); //checkout
                if (status > 0)
                {
                    TempData["Success"] = "Checkout success.";
                    ViewData["AnnounceMessage"] = "Checkout success.";
                }
                else
                {
                    ViewData["AnnounceMessage"] = "Checkout fail.";
                }
                return Page();
            }
            else
            {
                // Password is incorrect, display an error message or take appropriate action
                ViewData["AnnounceMessage"] = "Incorrect password.";
                return Page();
            }
        }


        public bool CheckPassword(string password)
        {
            string username = User.Identity.Name;
            var employee = _employeeRepository.GetEmployees().Where(x => x.UserName == username);
            if (employee.Count() > 0)
            {
                if (BCrypt.Net.BCrypt.Verify(username + password, employee.First().Password))
                {
                    return true;
                }
            }
            return false;
        }

        public WorkShiftEmployee GetWorkShiftEmployee()
        {
            string username = User.Identity.Name;
            int workshiftEmpId = HttpContext.Session.GetInt32("WorkShiftEmployeeId").Value;
            var empId = _employeeRepository.GetEmployees().Where(x => x.UserName == username).Select(x => x.EmpId).First();
            var workshiftEmployee = _workShiftEmployeeRepository.GetWorkShiftEmployees().Where(x => x.EmpId == empId && x.WsempId == workshiftEmpId).First();
            if (workshiftEmployee != null)
            {
                return workshiftEmployee;
            }
            return null;
        }
    }
}
