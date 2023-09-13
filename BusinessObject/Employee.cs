using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeBonus = new HashSet<EmployeeBonu>();
            Orders = new HashSet<Order>();
            RequestApproverNavigations = new HashSet<Request>();
            RequestRequesterNavigations = new HashSet<Request>();
            Salaries = new HashSet<Salary>();
            SalaryReports = new HashSet<SalaryReport>();
            WorkShiftEmployees = new HashSet<WorkShiftEmployee>();
        }

        public int EmpId { get; set; }
        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime FirstDateAtWork { get; set; }
        public string Image { get; set; }
        public int Salary { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime SalaryUpdateDate { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int PosId { get; set; }

        public virtual Position Pos { get; set; }
        public virtual ICollection<EmployeeBonu> EmployeeBonus { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Request> RequestApproverNavigations { get; set; }
        public virtual ICollection<Request> RequestRequesterNavigations { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
        public virtual ICollection<SalaryReport> SalaryReports { get; set; }
        public virtual ICollection<WorkShiftEmployee> WorkShiftEmployees { get; set; }
    }
}
