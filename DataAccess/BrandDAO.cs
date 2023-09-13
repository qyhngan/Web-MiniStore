using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BrandDAO
    {
        private static BrandDAO instance = null;
        private static readonly object instanceLock = new object();

        public static BrandDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BrandDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Brand> GetBrands()
        {
            List<Brand> brands;
            try
            {
                var dbContext = new MiniStoreContext();
                brands = dbContext.Brands.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return brands;
        }
    }
}
