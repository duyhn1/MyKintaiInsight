using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mykintai_Indentỉy.Models.Home
{
    public class OvertimeModel
    {
        public int employeeId { get; set; }
        public string requestDate { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string startTimeEdit { get; set; }
        public string endTimeEdit { get; set; }
        //public string dayOfWeek { get; set; }
        //public int totalOvertime { get; set; }
        //public int normalOvertime { get; set; }
        public int status { get; set; }
        public string reason { get; set; }
        public string approver { get; set; }
        public string account { get; set; }
        public string notes { get; set; }
        public int id { get; set; }
        public string createBy { get; set; }
        public string modifiedBy { get; set; }
    }
}