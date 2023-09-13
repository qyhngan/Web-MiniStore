using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repository;
using Repository.Implement;
using Utils;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace MiniStoreRazorPage.Pages.ManageWorkShiftEmployee
{
    [Authorize(Roles = "Manager, Sales, Guard")]
    public class RequestListModel : PageModel
    {
        public string Role { get; set; }
        IRequestRepository _requestRepository = new RequestRepository();
        IWorkShiftEmployeeRepository _workShiftEmployeeRepository = new WorkShiftEmployeeRepository();
        IEmployeeRepository _employeeRepository = new EmployeeRepository();
        IWorkShiftRepository _workShiftRepository = new WorkShiftRepository();

        [BindProperty]
        public List<RequestModel> Requests { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int totalPages { get; set; } = 1;

        [Authorize(Roles = "Manager")]
        public async Task OnGetAsync()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            Role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;
            int curEmpId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            BusinessObject.Employee curEmp = _employeeRepository.GetEmployees().Where(x => x.EmpId == curEmpId).FirstOrDefault();
            List<Request> r = _requestRepository.GetRequests().OrderByDescending(x => x.CreatedTime).ToList();

            if (r == null)
            {
                RedirectToPage("./Index");
            }

            if (Role == "Sales" || Role == "Guard")
            {
                int employeeId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                r = r.Where(r => r.Requester == employeeId).ToList();
            }

            Requests = new List<RequestModel>();
            foreach (Request request in r)
            {

                RequestModel rq = new RequestModel();
                rq.RequestId = request.RequestId;
                rq.Type = Utils.Status.RequestType.GetValueOrDefault(request.Type);
                rq.StartDate = request.StartDate;
                rq.EndDate = request.EndDate;
                rq.PartialDay = Status.PartialDay.GetValueOrDefault(request.PartialDay);
                rq.Reason = Utils.Status.ReasonType.GetValueOrDefault(request.Reason);
                rq.DetailReason = request.DetailReason;
                if (request.Approver == null)
                {
                    rq.Approver = "Chưa xét duyệt";
                }
                else
                {
                    rq.Approver = request.ApproverNavigation.FullName + " - " + request.Approver;
                }
                rq.Requester = request.RequesterNavigation.FullName + " - " + request.Requester;
                rq.Status = Utils.Status.RequestStatus.GetValueOrDefault(request.Status);
                rq.CreatedTime = request.CreatedTime;
                rq.ModifiedTime = request.ModifiedTime;
                if (rq.StartDate <= DateTime.Now)
                {
                    rq.isButton = 0;
                }

                rq.CreatedBy = request.CreatedBy;
                rq.StatusId = request.Status;
                BusinessObject.Employee m = _employeeRepository.GetEmployeeById(request.ModifiedBy);
                rq.ModifiedBy = m.FullName + " - " + m.EmpId;
                Requests.Add(rq);

                // Calculate the total number of pages
                totalPages = (int)Math.Ceiling(Requests.Count / (double)PageSize);

                // Ensure PageNumber is within valid range
                PageNumber = Math.Max(1, Math.Min(PageNumber, totalPages));

                // Skip items that are before the current page and take the specified number of items for the current page
                Requests = Requests.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();


            }
        }

        public async Task OnPostAsync()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            Role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;
            int curEmpId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            BusinessObject.Employee curEmp = _employeeRepository.GetEmployees().Where(x => x.EmpId == curEmpId).FirstOrDefault();
            List<Request> r = _requestRepository.GetRequests().OrderByDescending(x => x.CreatedTime).ToList();

            if (r == null)
            {
                RedirectToPage("./Index");
            }

            if (Role == "Sales" || Role == "Guard")
            {
                int employeeId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                r = r.Where(r => r.Requester == employeeId).ToList();
            }
            else
            {
                DateTime fromDate;
                DateTime toDate;
                if (Request.Form["fromDate"] != "")
                {
                    fromDate = DateTime.Parse(Request.Form["fromDate"]);
                    r = r.Where(r => r.CreatedTime >= fromDate).ToList();
                }

                if (Request.Form["toDate"] != "")
                {
                    toDate = DateTime.Parse(Request.Form["toDate"]);
                    r = r.Where(r => r.CreatedTime <= toDate).ToList();
                }

                string statusSelect = Request.Form["status"];

                if (statusSelect == "1")
                {
                    r = r.Where(r => r.Status == 0).ToList();
                }
                else if (statusSelect == "2")
                {
                    r = r.Where(r => r.Status == 1).ToList();
                }
                else if (statusSelect == "3")
                {
                    r = r.Where(r => r.Status == 2).ToList();
                }
                else if (statusSelect == "4")
                {
                    r = r.Where(r => r.Status == 3).ToList();
                }
            }

            Requests = new List<RequestModel>();
            foreach (Request request in r)
            {

                RequestModel rq = new RequestModel();
                rq.RequestId = request.RequestId;
                rq.Type = Utils.Status.RequestType.GetValueOrDefault(request.Type);
                rq.StartDate = request.StartDate;
                rq.EndDate = request.EndDate;
                rq.PartialDay = Status.PartialDay.GetValueOrDefault(request.PartialDay);
                rq.Reason = Utils.Status.ReasonType.GetValueOrDefault(request.Reason);
                rq.DetailReason = request.DetailReason;
                if (request.Approver == null)
                {
                    rq.Approver = "Chưa xét duyệt";
                }
                else
                {
                    rq.Approver = request.ApproverNavigation.FullName + " - " + request.Approver;
                }
                rq.Requester = request.RequesterNavigation.FullName + " - " + request.Requester;
                rq.Status = Utils.Status.RequestStatus.GetValueOrDefault(request.Status);
                rq.CreatedTime = request.CreatedTime;
                rq.ModifiedTime = request.ModifiedTime;
                rq.CreatedBy = request.CreatedBy;
                rq.StatusId = request.Status;
                BusinessObject.Employee m = _employeeRepository.GetEmployeeById(request.ModifiedBy);
                rq.ModifiedBy = m.FullName + " - " + m.EmpId;
                Requests.Add(rq);

            }


            // Calculate the total number of pages
            int totalPages = (int)Math.Ceiling(Requests.Count / (double)PageSize);

            // Ensure PageNumber is within valid range
            PageNumber = Math.Max(1, Math.Min(PageNumber, totalPages));

            // Skip items that are before the current page and take the specified number of items for the current page
            //Requests = Requests.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }

        [Authorize(Roles = "Manager")]
        public IActionResult OnPostApproveRequest()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            string curEmpID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int rid = int.Parse(Request.Form["rid"]);
            if (!string.IsNullOrEmpty(curEmpID))
            {
                Request request = _requestRepository.GetRequestById(rid);
                if (request != null)
                {
                    var wse = _workShiftEmployeeRepository.GetWorkShiftEmployeesByEmpIdAndDate(request.Requester, request.StartDate, request.EndDate);
                    WorkShiftEmployee workShiftEmployee;
                    foreach (var item in wse)
                    {
                        if (_workShiftRepository.GetWorkShifts().Where(x => x.WorkShiftId == item.WorkShiftId && x.StartTime.Equals(request.StartDate.TimeOfDay)) != null)
                        {
                            workShiftEmployee = item;
                            workShiftEmployee.EmpId = 0;
                            workShiftEmployee.Status = 0;
                            _workShiftEmployeeRepository.UpdateWorkShiftEmployee(workShiftEmployee);
                        }
                    }
                    request.Approver = int.Parse(curEmpID);
                    request.Status = 1;
                    request.ModifiedTime = DateTime.Now;
                    request.ModifiedBy = int.Parse(curEmpID);
                    _requestRepository.UpdateRequest(request);
                    return RedirectToPage("./Index");
                }
                else
                {
                    ViewData["Message"] = "Không tìm thấy yêu cầu";
                    return RedirectToPage("./RequestList");
                }
            }
            return RedirectToPage("./Index");
        }

        [Authorize(Roles = "Manager")]
        public IActionResult OnPostRejectRequest()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            string curEmpID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int rid = int.Parse(Request.Form["rid"]);
            if (!string.IsNullOrEmpty(curEmpID))
            {
                Request request = _requestRepository.GetRequestById(rid);
                if (request != null)
                {
                    request.Approver = int.Parse(curEmpID);
                    request.Status = 2;
                    request.ModifiedTime = DateTime.Now;
                    request.ModifiedBy = int.Parse(curEmpID);
                    _requestRepository.UpdateRequest(request);
                    TempData["Success"] = "Từ chối yêu cầu thành công";
                    return RedirectToPage("./RequestList");
                }
                else
                {
                    TempData["Success"] = "Không tìm thấy yêu cầu";
                    return RedirectToPage("./RequestList");
                }
            }
            return RedirectToPage("./RequestList");
        }

        [Authorize(Roles = "Sales, Guard")]
        public IActionResult OnPostCancelRequest()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            string curEmpID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int rid = int.Parse(Request.Form["rid"]);
            if (!string.IsNullOrEmpty(curEmpID))
            {
                Request request = _requestRepository.GetRequestById(rid);
                if (request != null)
                {
                    request.Approver = int.Parse(curEmpID);
                    request.Status = 3;
                    request.ModifiedTime = DateTime.Now;
                    request.ModifiedBy = int.Parse(curEmpID);
                    _requestRepository.UpdateRequest(request);
                    return RedirectToPage("./RequestList");
                }
                else
                {
                    ViewData["Message"] = "Không tìm thấy yêu cầu";
                    return RedirectToPage("./RequestList");
                }
            }
            return RedirectToPage("./RequestList");
        }

        [Authorize(Roles = "Sales, Guard")]
        public IActionResult OnPostEditRequest()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            string curEmpID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int rid = int.Parse(Request.Form["rid"]);
            if (!string.IsNullOrEmpty(curEmpID))
            {
                Request request = _requestRepository.GetRequestById(rid);
                if (request != null)
                {
                    request.Approver = int.Parse(curEmpID);
                    request.Status = 3;
                    request.ModifiedTime = DateTime.Now;
                    request.ModifiedBy = int.Parse(curEmpID);
                    _requestRepository.UpdateRequest(request);
                    return RedirectToPage("./RequestList");
                }
                else
                {
                    ViewData["Message"] = "Không tìm thấy yêu cầu";
                    return RedirectToPage("./RequestList");
                }
            }
            return RedirectToPage("./RequestList");
        }
    }

    public class RequestModel
    {
        public int RequestId { get; set; }
        public string Type { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public string PartialDay { get; set; }
        public string Reason { get; set; }
        public string DetailReason { get; set; }
        public string? Approver { get; set; }
        public string Requester { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public int CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public int isButton { get; set; } // 0: quá  hạn , 1: chưa quá hạn
    }
}
