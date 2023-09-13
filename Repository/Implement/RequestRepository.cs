using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class RequestRepository : IRequestRepository
    {
        public IEnumerable<Request> GetRequests() => RequestDAO.Instance.GetRequests();

        public int AddRequest(Request request) => RequestDAO.Instance.AddRequest(request);

        public Request GetRequestById(int requestId) => RequestDAO.Instance.GetRequestById(requestId);

        public int UpdateRequest(Request request) => RequestDAO.Instance.UpdateRequest(request);
    }
}
