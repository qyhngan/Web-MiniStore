using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();

        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Product> GetProducts()
        {
            List<Product> products;
            try
            {
                var dbContext = new MiniStoreContext();
                products = dbContext.Products.Include(x => x.Category).Include(x => x.Brand).Include(x => x.Origin).Include(x => x.Location).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public int DeleteProduct(int pId)
        {
            int status = 0;
            try
            {
                var dbContext = new MiniStoreContext();
                Product product = dbContext.Products.Where(x => x.ProductId == pId).FirstOrDefault();
                if (product != null)
                {
                    dbContext.Products.Remove(product);
                    status = dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return status;
        }

        public int AddProduct(Product product)
        {
            int status = 0;
            try
            {
                var dbContext = new MiniStoreContext();
                dbContext.Products.Add(product);
                status = dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return status;
        }

        public int UpdateProduct(Product product)
        {
            int status = 0;
            try
            {
                var dbContext = new MiniStoreContext();
                Product product1 = dbContext.Products.Where(x => x.ProductId == product.ProductId).FirstOrDefault();
                if (product1 != null)
                {
                    product1.CategoryId = product.CategoryId;
                    product1.BrandId = product.BrandId;
                    product1.OriginId = product.OriginId;
                    product1.LocationId = product.LocationId;
                    product1.Name = product.Name;
                    product1.UnitPrice = product.UnitPrice;
                    product1.Tax = product.Tax;
                    product1.UnitType = product.UnitType;
                    product1.DiscountAmount = product.DiscountAmount;
                    product1.InStock = product.InStock;
                    product1.SaleCount = product.SaleCount;
                    product1.Status = product.Status;
                    product1.Description = product.Description;
                    product1.ModifiedDate = DateTime.Now;
                    product1.ModifiedBy = product.ModifiedBy;
                    product1.ImgUrl = product.ImgUrl;
                    status = dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return status;
        }
    }
}
