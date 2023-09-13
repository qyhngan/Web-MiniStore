using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace MiniStoreRazorPage.Pages.Account
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage("/Account/Login");
        }

        ////public async Task<IActionResult> OnPostAsync()
        ////{
        ////    await HttpContext.SignOutAsync();
        ////    return RedirectToPage("/Account/Login");
        ////}
    }
}
                                               