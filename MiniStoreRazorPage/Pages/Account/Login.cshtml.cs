using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Repository;
using Repository.Implement;
using System.Linq;
using System;

namespace MiniStoreRazorPage.Pages.Account
{
    public class LoginModel : PageModel
    {
        IEmployeeRepository _employeeRepository = new EmployeeRepository();
        IPositionRepository _positionRepository = new PositionRepository();

        public string ErrorMessage { get; private set; }

        public async Task<IActionResult> OnPostAsync(string username, string password)
        {
            // Perform your authentication logic here, such as checking against a database

            if (IsValidUser(username, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    // Add additional claims as neededs 
                };
                var role = GetRole(username);
                claims.Add(new Claim(ClaimTypes.NameIdentifier, GetEmpId(username).ToString()));
                claims.Add(new Claim(ClaimTypes.Role, role));
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                //Set ID in session memory
                HttpContext.Session.Set("AccID", System.Text.Encoding.UTF8.GetBytes(GetID(username).ToString()));

                await HttpContext.SignInAsync(principal);

                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Đăng nhập thất bại";
                return Page();
            }
        }

        private int GetID(string username)
        {
            int posId = _employeeRepository.GetEmployees().Where(x => x.UserName == username).Select(x => x.EmpId).First();
            return posId;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                //// Check if the user has the specified claim
                //if (User.HasClaim(c => c.Type == "Role" && !string.IsNullOrEmpty(c.Value)))
                //{
                    // Redirect authenticated users with the specified claim to another page
                    return RedirectToPage("/Index");
                //}
            }

            return Page();
        }

        private bool IsValidUser(string username, string password)
        {
            var user = _employeeRepository.GetEmployee(username, password);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GetRole(string username)
        {
            var posId = _employeeRepository.GetEmployees().Where(x => x.UserName == username).Select(x => x.PosId).First();
            var posName = _positionRepository.GetPositions().Where(x => x.PosId == posId);
            if (posName.Count() > 0)
            {
                return posName.First().Name;
            }
            else
            {
                return "User";
            }
        }

        private int GetEmpId(string username)
        {
            return _employeeRepository.GetEmployees().Where(x => x.UserName == username).Select(x => x.EmpId).First();
        }


    }
}
