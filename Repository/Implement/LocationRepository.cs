using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class LocationRepository : ILocationRepository
    {
        public Location GetLocationById(int id) => LocationDAO.Instance.GetLocations().Where(x => x.LocationId == id).FirstOrDefault();

        public IEnumerable<Location> GetLocations() => LocationDAO.Instance.GetLocations();
    }
}
