using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.Implement;
using System;
using System.Linq;

namespace MiniStoreRazorPage.Pages.Employee
{
    public class UpdateModel : PageModel
    {
        public IActionResult OnPostCreateSalary(string empId)
        {
            HttpContext.Session.SetString("EmployeeID", empId);

            return RedirectToPage("/Salary/Create");
        }

        public IActionResult OnPostViewSalary(string salaryId)
        {
            HttpContext.Session.SetString("SalaryID", salaryId);

            return RedirectToPage("/Salary/View");
        }

        public IActionResult OnPostUpdateEmployeeStatus(string empId)
        {
            //Get ID from Session memory
            int ID = int.Parse(HttpContext.Session.GetString("AccID"));
            //Update employee status = 2 with empId
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            BusinessObject.Employee employee = employeeRepository.GetEmployees().Where(x => x.EmpId == Int32.Parse(empId)).FirstOrDefault();
            employee.Status = 2;
            employee.ModifiedBy = ID;
            employee.ModifiedDate = DateTime.Now;
            //Update employee to the repository
            int status = employeeRepository.UpdateEmployee(employee);
            if (status > 0)
            {
                ViewData["Message"] = "Kích hoạt thành công nhân viên : " + employee.FullName;
            }
            else
            {
                ViewData["Message"] = "Có lỗi đã xảy ra, vui lòng thử lại sau ít phút";
            }
            return Page();
        }

        public IActionResult OnPostUpdateEmployee()
        {
            //Get ID from Session memory
            int ID = int.Parse(HttpContext.Session.GetString("AccID"));
            //Get employee id from session
            int empId = Convert.ToInt32(HttpContext.Session.GetString("EmployeeID"));
            //Get employee from the repository
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            BusinessObject.Employee employee = employeeRepository.GetEmployees().Where(x => x.EmpId == empId).FirstOrDefault();

            //Update employee from the form data
            employee.FullName = Request.Form["FullName"];
            string newPhoneNumber = Request.Form["PhoneNumber"];
            if (Request.Form["Birthday"] != "")
            {
                employee.Birthday = DateTime.Parse(Request.Form["Birthday"]);
            }
            employee.Address = Request.Form["Address"];
            employee.PosId = int.Parse(Request.Form["PosId"]);
            string newUserName = Request.Form["UserName"];
            employee.ModifiedBy = ID;
            employee.ModifiedDate = DateTime.Now;

            //if newUserName is not equal to old username, check duplicate
            if (newUserName != employee.UserName)
            {
                //check does username duplicate in database
                var checkDuplicate = employeeRepository.GetEmployees().Where(x => x.UserName == newUserName);
                if (checkDuplicate.Count() > 0)
                {
                    ViewData["UserName"] = "Username inputed is duplicate";
                    return Page();
                }
            }
            employee.UserName = newUserName;

            //if phoneNumber is not equal to old phoneNumber, check duplicate
            if (newPhoneNumber != employee.PhoneNumber)
            {
                //check does phoneNumber duplicate in database
                var checkDuplicate = employeeRepository.GetEmployees().Where(x => x.PhoneNumber == newPhoneNumber);
                if (checkDuplicate.Count() > 0)
                {
                    ViewData["PhoneNumber"] = "Phone number inputed is duplicate";
                    return Page();
                }
            }
            employee.PhoneNumber = newPhoneNumber;

            //Update employee to the repository
            int status = employeeRepository.UpdateEmployee(employee);
            if (status > 0)
            {
                ViewData["Message"] = "Update success employee : " + employee.FullName;
            }
            else
            {
                ViewData["Message"] = "Update fail";
            }

            return Page();
        }
    }
}
