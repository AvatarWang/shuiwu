using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Param
{
    public class ReportParam
    {
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }
}
