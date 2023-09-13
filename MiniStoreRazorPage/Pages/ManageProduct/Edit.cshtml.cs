using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repository;
using Repository.Implement;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MiniStoreRazorPage.Pages.ManageProduct
{
    [Authorize(Roles = "Manager")]
    public class EditModel : PageModel
    {
        IProductRepositoryI _productRepositoryI = new ProductRepository();

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = _productRepositoryI.GetProduct((int)id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product.ModifiedDate = DateTime.Now;

            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            string id = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Product.ModifiedBy = int.Parse(id);
            int status = _productRepositoryI.UpdateProduct(Product);
            if (status > 0)
            {
                ViewData["SuccessUpdate"] = "Cập nhật thành công!";
            }
            else
            {
                ViewData["FailUpdate"] = "Cập nhật thất bại!";
            }
            return Page();
        }
    }
}
