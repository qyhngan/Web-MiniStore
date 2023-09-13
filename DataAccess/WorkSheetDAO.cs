using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class WorkSheetDAO
    {
        private static WorkSheetDAO instance = null;
        private static readonly object instanceLock = new object();

        public static WorkSheetDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new WorkSheetDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Worksheet> GetWorksheets()
        {
            List<Worksheet> worksheets;
            try
            {
                var dbContext = new MiniStoreContext();
                worksheets = dbContext.Worksheets.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return worksheets;
        }
    }
}
