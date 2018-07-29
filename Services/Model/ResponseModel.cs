using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public enum RspType
    {
        success,
        alert,
        error,
        info,
        danger
    }

    public enum RspCode
    {
        C0000 = 0,
        C0001 = 1,
        C0002 = 2,
        C0003 = 3,
        C0004 = 4
    }

    public class ResponseModel<T> : ResponseModel
    {
        public T Body { get; set; }
        public PageModel Page { get; set; }
    }
    public class PageModel
    {
        public int PageSize { set; get; } = 15;

        public int PageCount
        {
            get
            {
                if (TotalCount % PageSize == 0)
                    return TotalCount / PageSize;
                else
                    return TotalCount / PageSize + 1;
            }
        }
        public int TotalCount { get; set; }
    }
    public class ResponseModel
    {
        public RspType RspType { get; set; } = RspType.success;

        public string type { get { return RspType.ToString(); } }

        public RspCode RspCode { get; set; } = RspCode.C0000;

        public string RspMsg { get; set; } = "操作成功";
    }
}