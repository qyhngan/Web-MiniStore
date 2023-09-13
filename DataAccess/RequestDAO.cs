using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RequestDAO
    {
        private static RequestDAO instance = null;
        private static readonly object instanceLock = new object();

        public static RequestDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RequestDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Request> GetRequests()
        {
            List<Request> requests;
            try
            {
                var dbContext = new MiniStoreContext();
                requests = dbContext.Requests
                .Include(r => r.ApproverNavigation)
                .Include(r => r.RequesterNavigation).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return requests;
        }

        public int AddRequest(Request request)
        {
            int status = 0;
            try
            {
                var dbContext = new MiniStoreContext();
                dbContext.Requests.Add(request);
                status = dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return status;
        }

        public int UpdateRequest(Request request)
        {
            int status = 0;
            try
            {
                var dbContext = new MiniStoreContext();
                Request r = dbContext.Requests.Where(x => x.RequestId == request.RequestId).FirstOrDefault();
                if (r != null)
                {
                    r.Type = request.Type;
                    r.StartDate = request.StartDate;
                    r.EndDate = request.EndDate;
                    r.PartialDay = request.PartialDay;
                    r.Reason = request.Reason;
                    r.DetailReason = request.DetailReason;
                    r.Approver = request.Approver;
                    r.Status = request.Status;
                    r.ModifiedTime = DateTime.Now;
                    r.ModifiedBy = request.ModifiedBy;

                    status = dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return status;
        }

        public Request GetRequestById(int rId)
        {
            Request request;
            try
            {
                var dbContext = new MiniStoreContext();
                request = dbContext.Requests
                .Include(r => r.ApproverNavigation)
                .Include(r => r.RequesterNavigation).Where(x => x.RequestId == rId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return request;
        }
    }
}
