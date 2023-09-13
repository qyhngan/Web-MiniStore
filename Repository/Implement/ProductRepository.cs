using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class ProductRepository : IProductRepositoryI
    {
        public int AddProduct(Product product) => ProductDAO.Instance.AddProduct(product);

        public int DeleteProduct(int id) => ProductDAO.Instance.DeleteProduct(id);

        public Product GetProduct(int id) => GetProducts().Where(x => x.ProductId == id).FirstOrDefault();

        public IEnumerable<Product> GetProducts() => ProductDAO.Instance.GetProducts();

        public int UpdateProduct(Product product) => ProductDAO.Instance.UpdateProduct(product);
    }
}
