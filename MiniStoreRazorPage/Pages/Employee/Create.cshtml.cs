using BusinessObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.Implement;
using System;
using System.Data;
using System.Linq;
using System.Security.Claims;

namespace MiniStoreRazorPage.Pages.Employee
{
    [Authorize(Roles = "Manager")]
    public class CreateModel : PageModel
    {

        public IActionResult OnPostAddEmployee()
        {
            //Get ID from Session memory
            int ID = int.Parse(HttpContext.Session.GetString("AccID"));
            // Get data from form
            BusinessObject.Employee employee = new BusinessObject.Employee();
            employee.FullName = Request.Form["FullName"];
            if (Request.Form["Birthday"] != "")
            {
                employee.Birthday = DateTime.Parse(Request.Form["Birthday"]);
            }
            string newPhoneNumber = Request.Form["PhoneNumber"];
            employee.Address = Request.Form["Address"];
            string newUserName = Request.Form["UserName"];
            ViewData["InputedData"] = employee;
            if (Request.Form["PosId"] != "")
            {
                employee.PosId = int.Parse(Request.Form["PosId"]);
            }
            else
            {
                ViewData["PositionId"] = "Position must have value";
                return Page();
            }
            

            //check does username duplicate in database
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            //if newUserName is not equal to old username, check duplicate
            if (newUserName != employee.UserName)
            {
                //check does username duplicate in database
                var checkDuplicate = employeeRepository.GetEmployees().Where(x => x.UserName == newUserName);
                if (checkDuplicate.Count() > 0)
                {
                    ViewData["UserName"] = "This Username has been used";
                    employee.UserName = newUserName;
                    ViewData["InputedData"] = employee;
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
                    ViewData["PhoneNumber"] = "This Phone number has been used";
                    employee.PhoneNumber = newPhoneNumber;
                    ViewData["InputedData"] = employee;
                    return Page();
                }
            }
            employee.PhoneNumber = newPhoneNumber;
            ViewData["InputedData"] = employee;

            employee.Password = BCrypt.Net.BCrypt.HashPassword(newUserName+"1");
            //list all remaining field of employee
            employee.FirstDateAtWork = DateTime.Now;
            employee.Salary = 0;
            employee.SalaryUpdateDate = DateTime.MinValue;
            employee.Status = 2;
            employee.CreatedDate = DateTime.Now;
            employee.ModifiedDate = DateTime.Now;
            employee.CreatedBy = ID;
            employee.ModifiedBy = ID;


            // Insert employee to database
            int status = employeeRepository.AddEmployee(employee);


            //If status >0 return update success to page
            if (status > 0)
            {
                ViewData["Message"] = "Tạo nhân viên thành công";
            }
            else
            {
                ViewData["Message"] = "Tạo nhân viên thất bại";
            }
            return Page();
        }
    }
}
