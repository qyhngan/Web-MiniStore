using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Implement;
using Repository;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;

namespace MiniStoreRazorPage.Pages.Employee
{
    [Authorize(Roles = "Sales, Guard")]
    public class ChangePasswordModel : PageModel
    {
        IEmployeeRepository _employeeRepository = new EmployeeRepository();

        public IActionResult OnPost()
        {
            //Get ID from Session memory
            int ID = int.Parse(HttpContext.Session.GetString("AccID"));
            //Get employee from the repository
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            BusinessObject.Employee employee = employeeRepository.GetEmployees().Where(x => x.EmpId == ID).FirstOrDefault();

            if (!BCrypt.Net.BCrypt.Verify(employee.UserName + Request.Form["OldPassword"], employee.Password)){
                ViewData["ErrorMessage"] = "Mật khẩu hiện tại chưa chính xác";
                return Page();
            }

            if (Request.Form["NewPassword"] != Request.Form["ConfirmNewPassword"])
            {
                ViewData["ErrorMessage"] = "Mật khẩu nhập lại chưa chính xác";
                return Page();
            }

            employee.Password = BCrypt.Net.BCrypt.HashPassword(employee.UserName + Request.Form["NewPassword"]);

            //Update employee to the repository
            int status = employeeRepository.UpdateEmployee(employee);
            if (status > 0)
            {
                ViewData["Message"] = "Cập nhật mật khẩu thành công.";
            }
            else
            {
                ViewData["ErrorMessage"] = "Cập nhật mật khẩu thất bại!";
            }

            return Page();
        }
    }
}
