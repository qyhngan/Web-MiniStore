using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class OriginRepository : IOriginRepository
    {
        public Origin GetOrigin(int id) => GetOrigins().Where(x => x.OriginId == id).FirstOrDefault();

        public IEnumerable<Origin> GetOrigins() => OriginDAO.Instance.GetOrigins();
    }
}
