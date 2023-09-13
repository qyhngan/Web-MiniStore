using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class WorkShiftDAO
    {
        private static WorkShiftDAO instance = null;
        private static readonly object instanceLock = new object();

        public static WorkShiftDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new WorkShiftDAO();
                    }
                    return instance;
                }
            }
        }

        public List<WorkShift> GetWorkShifts()
        {
            List<WorkShift> workShifts;
            try
            {
                var dbContext = new MiniStoreContext();
                workShifts = dbContext.WorkShifts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return workShifts;
        }
    }
}
