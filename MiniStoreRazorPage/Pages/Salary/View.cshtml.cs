using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniStoreRazorPage.Pages.Salary
{
    public class ViewModel : PageModel
    {
        public IActionResult OnPostCancelSalary(string salaryId)
        {
            //Cancel salary
            Repository.ISalaryRepository salaryRepository = new Repository.Implement.SalaryRepository();
            int status = salaryRepository.CancelSalary(int.Parse(salaryId));
            if (status > 0)
            {
                ViewData["Cancel"] = "Hủy hợp đồng thành công";
            }
            else
            {
                ViewData["Cancel"] = "Đã có lỗi xảy ra, vui lòng thử lại sau ít phút";
            }
            return Page();
        }

        public IActionResult OnPostDenideSalary(string salaryId)
        {
            //Cancel salary
            Repository.ISalaryRepository salaryRepository = new Repository.Implement.SalaryRepository();
            int status = salaryRepository.CancelSalary(int.Parse(salaryId));
            if (status > 0)
            {
                ViewData["Cancel"] = "Từ chối thành công";
            }
            else
            {
                ViewData["Cancel"] = "Đã có lỗi xảy ra, vui lòng thử lại sau ít phút";
            }
            return Page();
        }

        public IActionResult OnPostConfirmSalary(string salaryId)
        {
            //Confirm salary
            Repository.ISalaryRepository salaryRepository = new Repository.Implement.SalaryRepository();
            int status = salaryRepository.ConfirmSalary(int.Parse(salaryId));
            if (status > 0)
            {
                ViewData["Confirm"] = "Đồng ý thành công";
            }
            else
            {
                ViewData["Confirm"] = "Đã có lỗi xảy ra, vui lòng thử lại sau ít phút";
            }
            return Page();
        }
    }
}
