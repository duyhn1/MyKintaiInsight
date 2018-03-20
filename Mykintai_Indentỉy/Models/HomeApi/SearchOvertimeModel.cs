using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mykintai_Indentỉy.Models.HomeApi
{
    public class SearchOvertimeModel
    {
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }

        public int status { get; set; }
    }
}