using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class WorkShiftEmployeeDAO
    {
        private static WorkShiftEmployeeDAO instance = null;
        private static readonly object instanceLock = new object();

        public static WorkShiftEmployeeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new WorkShiftEmployeeDAO();
                    }
                    return instance;
                }
            }
        }

        public List<WorkShiftEmployee> GetWorkShiftEmployees()
        {
            List<WorkShiftEmployee> workShiftEmployees;
            try
            {
                var dbContext = new MiniStoreContext();
                workShiftEmployees = dbContext.WorkShiftEmployees.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return workShiftEmployees;
        }

        public int UpdateLogWorkWorkShiftEmployee(int wseId, int typeButton)
        {
            int result = 0;
            try
            {
                var dbContext = new MiniStoreContext();
                var wseToUpdate = dbContext.WorkShiftEmployees.SingleOrDefault(wse => wse.WsempId == wseId);
                if (wseToUpdate != null)
                {
                    wseToUpdate.ModifiedTime = DateTime.Now;
                    if (typeButton == 0)
                    {
                        wseToUpdate.CheckinTime = DateTime.Now;
                    } 
                    else
                    {
                        wseToUpdate.CheckoutTime = DateTime.Now;
                    }
                    wseToUpdate.ModifiedBy = wseToUpdate.EmpId;
                    result = dbContext.SaveChanges(); 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public int AssignWorkShiftEmployee(int wsempId, int empId, int currentEmp)
        {
            int result = 0;
            try
            {
                var dbContext = new MiniStoreContext();
                var wseToUpdate = dbContext.WorkShiftEmployees.SingleOrDefault(wse => wse.WsempId == wsempId);
                if (wseToUpdate != null)
                {
                    wseToUpdate.EmpId = empId;
                    wseToUpdate.ModifiedTime = DateTime.Now;
                    wseToUpdate.ModifiedBy = currentEmp;
                    result = dbContext.SaveChanges();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<WorkShiftEmployee> GetWorkShiftEmployeesByEmpIdAndDate(int empId, DateTime startDate, DateTime endDate)
        {
            //Get WorkShiftEmployee by EmpId and Date
            IEnumerable<WorkShiftEmployee> workShiftEmployees;
            try
            {
                var dbContext = new MiniStoreContext();
                workShiftEmployees = dbContext.WorkShiftEmployees.Where(wse => wse.EmpId == empId && wse.Date >= startDate && wse.Date <= endDate).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return workShiftEmployees;
        }

        public int UpdateWorkShiftEmployee(WorkShiftEmployee workShiftEmployee)
        {
            int result = 0;
            try
            {
                var dbContext = new MiniStoreContext();
                var wseToUpdate = dbContext.WorkShiftEmployees.SingleOrDefault(wse => wse.WsempId == workShiftEmployee.WsempId);
                if (wseToUpdate != null)
                {
                    wseToUpdate.CheckinTime = workShiftEmployee.CheckinTime;
                    wseToUpdate.CheckoutTime = workShiftEmployee.CheckoutTime;
                    wseToUpdate.ModifiedTime = DateTime.Now;
                    wseToUpdate.StaffNum = workShiftEmployee.StaffNum;
                    wseToUpdate.Date = workShiftEmployee.Date;
                    wseToUpdate.EmpId = workShiftEmployee.EmpId;
                    wseToUpdate.Status = workShiftEmployee.Status;
                    wseToUpdate.ModifiedBy = workShiftEmployee.EmpId;
                    result = dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
