using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class UserReportModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Score { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
    }
}
