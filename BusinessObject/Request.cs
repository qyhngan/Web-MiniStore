using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Request
    {
        public int RequestId { get; set; }
        public int Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PartialDay { get; set; }
        public int Reason { get; set; }
        public string DetailReason { get; set; }
        public int? Approver { get; set; }
        public int Requester { get; set; }
        public int Status { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public virtual Employee ApproverNavigation { get; set; }
        public virtual Employee RequesterNavigation { get; set; }
    }
}
