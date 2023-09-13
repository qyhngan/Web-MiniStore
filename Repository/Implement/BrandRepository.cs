using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class BrandRepository : IBrandRepository
    {
        public IEnumerable<Brand> GetBrands() => BrandDAO.Instance.GetBrands();
    }
}
