using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Repository;
using Repository.Implement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MiniStoreRazorPage.Pages.ManageProduct
{
    [Authorize(Roles = "Manager")]
    public class CreateModel : PageModel
    {
        IProductRepositoryI _productRepository = new ProductRepository();
        ICategoryRepository _categoryRepository = new CategoryRepository();
        IBrandRepository    _brandRepository = new BrandRepository();
        IOriginRepository   _originRepository = new OriginRepository();
        ILocationRepository _locationRepository = new LocationRepository();

        public IActionResult OnGet()
        {
            var category = _categoryRepository.GetCategories().Select(x => new { x.CategoryId, V = x.Name}).ToList();
            SelectList sl1 = new SelectList(category, "CategoryId", "V");
            ViewData["CategoryCbo"] = sl1;

            var brand = _brandRepository.GetBrands().Select(x => new { x.BrandId, V = x.Name }).ToList();
            SelectList sl2 = new SelectList(brand, "BrandId", "V");
            ViewData["BrandCbo"] = sl2;

            var origin = _originRepository.GetOrigins().Select(x => new { x.OriginId, V = x.Province }).ToList();
            SelectList sl3 = new SelectList(origin, "OriginId", "V");
            ViewData["OriginCbo"] = sl3;

            var location = _locationRepository.GetLocations().Select(x => new { x.LocationId, V = x.Name }).ToList();
            SelectList sl4 = new SelectList(location, "LocationId", "V");
            ViewData["LocationCbo"] = sl4;

            SelectList sl5 = new SelectList(Utils.Status.UnitType, "Key", "Value");
            ViewData["UnitTypeCbo"] = sl5;
            return Page();
        }

        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public ProductCreate Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product product = new Product();

            var products = _productRepository.GetProducts();
            product.CreatedDate = DateTime.Now;
            product.ModifiedDate = DateTime.Now;
            product.Status = 1;
            product.ModifiedBy = int.Parse(HttpContext.Session.GetString("AccID"));
            product.CreatedBy = int.Parse(HttpContext.Session.GetString("AccID"));
            product.SaleCount = 0;
            product.InStock = Product.InStock;
            product.UnitPrice = Product.UnitPrice;
            product.Tax = Product.Tax*Product.UnitPrice/100;
            product.BrandId = Product.BrandId;
            product.CategoryId = Product.CategoryId;
            product.OriginId = Product.OriginId;
            product.LocationId = Product.LocationId;
            product.Name = Product.Name;
            product.UnitType = Product.UnitType;
            product.DiscountAmount = Product.DiscountAmount;
            product.Description = Product.Description;
            product.ImgUrl = Product.ImageFile;

            if (Product.UnitPrice < Product.DiscountAmount)
            {
                TempData["MessageError"] = "Giá khuyến mãi phải nhỏ hơn giá gốc";
                return Page();
            }

            int status = _productRepository.AddProduct(product);
            if (status > 0)
            {
                TempData["Message"] = "Tạo sản phẩm thành công";
            }
            else
            {
                TempData["MessageError"] = "Tạo sản phẩm thất bại";
            }
            return Page();

        }
    }

    public class ProductCreate
    {
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int OriginId { get; set; }
        public int LocationId { get; set; }
        [Required(ErrorMessage = "Không được trống")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Không được trống")]
        [Range(0, int.MaxValue, ErrorMessage = "The Unit Price must be greater than 0.")]
        public int UnitPrice { get; set; }
        [DefaultValue(0)]
        [Range(0, 10, ErrorMessage = "Thuế phải từ 0-10%.")]
        public int Tax { get; set; }
        public int UnitType { get; set; }
        [DefaultValue(0)]
        [Range(0, int.MaxValue, ErrorMessage = "Giá giảm không được âm")]
        public int DiscountAmount { get; set; }
        [DefaultValue(0)]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int InStock { get; set; }
        public int SaleCount { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string? ImageFile { get; set; }
    }
    
}

