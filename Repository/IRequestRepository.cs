using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRequestRepository
    {
        IEnumerable<Request> GetRequests();
        Request GetRequestById(int requestId);
        int AddRequest(Request request);
        int UpdateRequest(Request request);
    }
}
