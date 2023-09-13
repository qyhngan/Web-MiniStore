using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OriginDAO
    {
        private static OriginDAO instance = null;
        private static readonly object instanceLock = new object();

        public static OriginDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OriginDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Origin> GetOrigins()
        {
            List<Origin> categories;
            try
            {
                var dbContext = new MiniStoreContext();
                categories = dbContext.Origins.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return categories;
        }
    }
}
