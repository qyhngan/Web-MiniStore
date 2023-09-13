using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace MiniStoreRazorPage.Pages.Salary
{
    [Authorize(Roles = "Manager")]
    public class ManageModel : PageModel
    {
        public IActionResult OnPostCalculateSalary(string empId)
        {
            HttpContext.Session.SetString("EmployeeID", empId);

            return RedirectToPage("/Salary/Detail");
        }
    }
}
