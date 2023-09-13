using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.Implement;
using System.Data;
using System.Linq;
using BusinessObject;

namespace MiniStoreRazorPage.Pages.Employee
{
    [Authorize(Roles = "Manager")]
    public class ManageModel : PageModel
    {
        // create onPostUpdateEmployee method
        public IActionResult OnPostUpdateEmployee(string empId)
        {
            HttpContext.Session.SetString("EmployeeID", empId);

            return RedirectToPage("/Employee/Update");
        }

        // create onPostDeleteEmployee method
        public IActionResult OnPostDeleteEmployee(string empId)
        {
            IEmployeeRepository employees = new EmployeeRepository();
            //If flowerId not exist in any order detail, delete flower else update status to 0
            BusinessObject.Employee employee = employees.GetEmployees().Where(x => x.EmpId == int.Parse(empId)).FirstOrDefault();

            int status = employees.DeleteEmployee(int.Parse(empId));
            if (status > 0)
            {
                ViewData["Message"] = "Delete success employee : " + employee.FullName;
            }
            else
            {
                ViewData["Message"] = "Delete fail";
            }
            return Page();
        }
    }
}
