using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IOriginRepository
    {
        IEnumerable<Origin> GetOrigins();
        Origin GetOrigin(int id);
    }
}
