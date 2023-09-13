using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class Status
    {

        //Create Status Enum for Employee
        public static Dictionary<int, string> EmployeeStatus = new Dictionary<int, string>(
            new KeyValuePair<int, string>[]
            {
                new KeyValuePair<int, string>(0, "Đã nghỉ việc"),
                new KeyValuePair<int, string>(1, "Hoạt động"),
                new KeyValuePair<int, string>(2, "Tạm ngưng"),
                new KeyValuePair<int, string>(3, "Chờ gia hạn")
            });

        //Create Status Enum for Salary
        public static Dictionary<int, string> SalaryStatus = new Dictionary<int, string>(
            new KeyValuePair<int, string>[]
            {
                new KeyValuePair<int, string>(0, "Bản mẫu"),
                new KeyValuePair<int, string>(1, "Chờ xác nhận"),
                new KeyValuePair<int, string>(2, "Đã xác nhận"),
                new KeyValuePair<int, string>(3, "Hiệu lực"),
                new KeyValuePair<int, string>(4, "Hết hiệu lực"),
                new KeyValuePair<int, string>(5, "Hủy")
            });

        //Create Status Enum for EmployeeBonus
        public static Dictionary<int, string> EmployeeBonusStatus = new Dictionary<int, string>(
            new KeyValuePair<int, string>[]
            {
                new KeyValuePair<int, string>(0, "Đang xét"),
                new KeyValuePair<int, string>(1, "Đã xét")
            });

        //Create Status Enum for Bonus
        public static Dictionary<int, string> BonusStatus = new Dictionary<int, string>(
            new KeyValuePair<int, string>[]
            {
                new KeyValuePair<int, string>(0, "Đang xét"),
                new KeyValuePair<int, string>(1, "Đã xét")
            });
        //Create Status Enum for BonusCriteria
        public static Dictionary<int, string> BonusCriteriaStatus = new Dictionary<int, string>(
            new KeyValuePair<int, string>[]
            {
                new KeyValuePair<int, string>(0, "Bản mẫu"),
                new KeyValuePair<int, string>(1, "Hiệu lực"),
                new KeyValuePair<int, string>(2, "Không hiệu lực")
            });

        //Create Status Enum for Worksheet
        public static Dictionary<int, string> WorksheetStatus = new Dictionary<int, string>(
            new KeyValuePair<int, string>[]
            {
                new KeyValuePair<int, string>(0, "Chưa xác nhận"),
                new KeyValuePair<int, string>(1, "Đã xác nhận")
            });

        //Create Status Enum for WorkShift
        public static Dictionary<int, string> WorkShiftStatus = new Dictionary<int, string>(
            new KeyValuePair<int, string>[]
            {
                new KeyValuePair<int, string>(0, "Chưa xác nhận"),
                new KeyValuePair<int, string>(1, "Đã xác nhận")
            });

        //Create Status Enum for WorkShift_Employee
        public static Dictionary<int, string> WSEStatus = new Dictionary<int, string>(
            new KeyValuePair<int, string>[]
            {
                new KeyValuePair<int, string>(0, "Chưa xác nhận"),
                new KeyValuePair<int, string>(1, "Đã xác nhận")
            });

        //Create Status Enum for Request
        public static Dictionary<int, string> RequestStatus = new Dictionary<int, string>(
            new KeyValuePair<int, string>[]
            {
                new KeyValuePair<int, string>(0, "Đã gửi"),
                new KeyValuePair<int, string>(1, "Đã chấp thuận"),
                new KeyValuePair<int, string>(2, "Bị từ chối"),
                new KeyValuePair<int, string>(3, "Đã hủy")
            });

        //Create Enum for Position
        public static Dictionary<int, string> PositionID = new Dictionary<int, string>(
            new KeyValuePair<int, string>[]
            {
                new KeyValuePair<int, string>(1, "Quản lý"),
                new KeyValuePair<int, string>(2, "Nhân viên bán hàng"),
                new KeyValuePair<int, string>(3, "Bảo vệ"),
                new KeyValuePair<int, string>(4, "Khách hàng")
            });

        public static Dictionary<int, string> UnitType = new Dictionary<int, string>(
                       new KeyValuePair<int, string>[]
                       {
                new KeyValuePair<int, string>(1, "Cái"),
                new KeyValuePair<int, string>(2, "Kg"),
                new KeyValuePair<int, string>(3, "Chiếc"),
                new KeyValuePair<int, string>(4, "Cặp"),
                new KeyValuePair<int, string>(5, "L")
            });

        public static Dictionary<int, string> RequestType = new Dictionary<int, string>(
                       new KeyValuePair<int, string>[]
                       {
                new KeyValuePair<int, string>(0, "Nghỉ phép"),
                new KeyValuePair<int, string>(1, "Nghỉ bệnh"),
                new KeyValuePair<int, string>(2, "Nghỉ không lương")
            });

        public static Dictionary<int, string> ReasonType = new Dictionary<int, string>(
                       new KeyValuePair<int, string>[]
                       {
                new KeyValuePair<int, string>(0, "Chăm sóc người nhà ốm"),
                new KeyValuePair<int, string>(1, "Chăm sóc con ốm"),
                new KeyValuePair<int, string>(2, "Lý do sức khỏe cá nhân"),
                new KeyValuePair<int, string>(3, "Chế độ con nhỏ"),
                new KeyValuePair<int, string>(4, "Đưa đón con đi học"),
                new KeyValuePair<int, string>(5, "Khác"),
            });

        public static Dictionary<int, string> PartialDay = new Dictionary<int, string>(
                       new KeyValuePair<int, string>[]
                       {
                new KeyValuePair<int, string>(0, "Ca 1"),
                new KeyValuePair<int, string>(1, "Ca 2"),
                new KeyValuePair<int, string>(2, "Ca 3"), //sale mới có ca 3
                new KeyValuePair<int, string>(3, "Cả ngày")
            });
    }
}
