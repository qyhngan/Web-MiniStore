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
    public class IndexModel : PageModel
    {
        IProductRepositoryI _productRepository = new ProductRepository();
        IEmployeeRepository _employeeRepository = new EmployeeRepository();

        public IList<ProductModel> Product { get;set; }

        public async Task OnGetAsync()
        {
            var p = _productRepository.GetProducts().ToList();
            if (p.Count > 0)
            {
                Product = new List<ProductModel>();
                foreach (var item in p)
                {
                    ProductModel pd = new ProductModel();
                    pd.pd = item;
                    pd.CreatedName = _employeeRepository.GetEmployees().Where(x => x.EmpId == item.CreatedBy).Select(x => x.FullName).First().ToString() + " - " + item.CreatedBy;
                    pd.ModifiedName = _employeeRepository.GetEmployees().Where(x => x.EmpId == item.ModifiedBy).Select(x => x.FullName).First().ToString() + " - " + item.ModifiedBy;
                    if (item.Status == 1)
                    {
                        pd.PStatus = "Đang bán";
                    }
                    else
                    {
                        pd.PStatus = "Ngừng bán";
                    }
                    Product.Add(pd);
                }
            } 
        }
    }

    public class ProductModel : PageModel
    {
        public Product pd { get; set; }
        public string ModifiedName { get; set; }
        public string CreatedName { get; set; }
        public string PStatus { get; set; }
    }
}
