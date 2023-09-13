using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class LocationDAO
    {
        private static LocationDAO instance = null;
        private static readonly object instanceLock = new object();

        public static LocationDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new LocationDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Location> GetLocations()
        {
            List<Location> locations;
            try
            {
                var dbContext = new MiniStoreContext();
                locations = dbContext.Locations.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return locations;
        }
    }
}
