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

namespace MiniStoreRazorPage.Pages.Salary
{
    [Authorize(Roles = "Manager")]
    public class CreateModel : PageModel
    {
        public IActionResult OnPostAddSalary()
        {
            //Get ID from Session memory
            int ID = int.Parse(HttpContext.Session.GetString("AccID"));
            //Get data from form
            BusinessObject.Salary salary = new BusinessObject.Salary();
            salary.EmpId = Convert.ToInt32(HttpContext.Session.GetString("EmployeeID"));
            salary.Amount = int.Parse(Request.Form["Amount"]);
            salary.StartDate = DateTime.Parse(Request.Form["StartDate"]);
            salary.EndDate = DateTime.Parse(Request.Form["EndDate"]);
            ViewData["InputedData"] = salary;

            //Check is there and Salary of this Employee have status <= 2
            ISalaryRepository salaryRepository = new SalaryRepository();
            var count = salaryRepository.GetSalaries().Where(x => x.EmpId == salary.EmpId && x.Status <= 2).Count();
            if (count != 0)
            {
                ViewData["Message"] = "Nhân viên này đã có hợp đồng chờ đồng ý hoặc đã xác nhận";
                return Page();
            }

            //Check is EndDate before StartDate as least 30 days
            if (salary.EndDate < salary.StartDate)
            {
                ViewData["EndDate"] = "Ngày kết thúc hợp đồng phải sau ngày bắt đầu";
                return Page();
            }
            else if (salary.EndDate < salary.StartDate.AddDays(30))
            {
                ViewData["EndDate"] = "Ngày kết thúc phải dài hơn ít nhất 30 ngày so với ngày bắt đầu";
                return Page();
            }
            //Check is Salary must > 1000
            else if (salary.Amount < 1000)
            {
                ViewData["Amount"] = "Lương phải lớn hơn 1000VNĐ/giờ";
                return Page();
            }

            salary.SalaryUpdateDate = DateTime.Now;
            salary.CreatedBy = ID;
            salary.CreatedTime = DateTime.Now;
            salary.ModifiedBy = ID;
            salary.ModifiedTime = DateTime.Now;
            
            
            count = salaryRepository.GetSalaries().Where(x => x.EmpId == salary.EmpId).Count();
            if (count == 0)
            {
                salary.Status = 2;
            } else
            {
                salary.Status = 1;
            }

            //Add to database
            int status = salaryRepository.AddSalary(salary);


            //If status >0 return update success to page
            if (status > 0)
            {
                ViewData["Message"] = "Create new Salary successfully";
            }
            else
            {
                ViewData["Message"] = "Create fail";
            }
            return Page();
        }
    }
}
