using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repository;
using Repository.Implement;

namespace MiniStoreRazorPage.Pages.ManageProduct
{
    public class DeleteModel : PageModel
    {
        IProductRepositoryI _productRepository = new ProductRepository();
        IOrderDetailRepository _orderDetailRepository = new OrderDetailRepository();

        [BindProperty]
        public Product Product { get; set; }
        public bool IsDelete { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _orderDetailRepository.GetOrderDetails().Where(x => x.ProductId == id);
            if (order.Count() > 0)
            {
                IsDelete = false;
                TempData["Message"] = "Không thể xóa sản phẩm này vì đã có đơn hàng";
            } else
            {
                IsDelete = true;
            }

            Product = _productRepository.GetProduct((int)id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int status = 0;
            if (IsDelete)
            {
                status = _productRepository.DeleteProduct((int)id);
            }
            else
            {
                var product = _productRepository.GetProduct((int)id);
                product.Status = 0;
                status = _productRepository.UpdateProduct(product);
            }

            if (status != 0)
            {
                TempData["Success"] = "Xóa thành công";
            }
            else
            {
                ViewData["MessageFail"] = "Xóa thất bại";
            }

            return RedirectToPage("./Index");
        }
    }
}
