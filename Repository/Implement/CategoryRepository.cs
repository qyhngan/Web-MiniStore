using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class CategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> GetCategories() => CategoryDAO.Instance.GetCategories();

        public Category GetCategory(int id) => GetCategories().Where(x => x.CategoryId == id).FirstOrDefault();
    }
}
