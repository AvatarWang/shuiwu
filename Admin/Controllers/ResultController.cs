using Services;
using Services.Model;
using Services.Param;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class ResultController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult CheckLogin(string name, string pwd)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(pwd))
                return Json(new ResponseModel
                {
                    RspCode = RspCode.C0001
                }, JsonRequestBehavior.AllowGet);
            if (name.ToLower() != "admin" || pwd != "Jg5vW6")
                return Json(new ResponseModel
                {
                    RspCode = RspCode.C0001
                }, JsonRequestBehavior.AllowGet);

            return Json(new ResponseModel
            {
                RspCode = RspCode.C0000
            }, JsonRequestBehavior.AllowGet);
        }

        //[Json]
        [System.Web.Mvc.HttpPost]
        public JsonResult GetResult([FromBody]ReportParam param)
        {
            var where = new StringBuilder(" where 1=1 ");

            if (!string.IsNullOrWhiteSpace(param.name))
                where.Append(" and name = @name ");

            var sql = @" select u.ID as UserId,u.Name as UserName,us.Score,us.CreateTime,us.UpdateTime  FROM [WaterSupplySecurity].[dbo].[UserScore] us inner join [dbo].[User] u on u.ID = us.UserID   " + where.ToString();

            var result = DataAcccessHelper.Query<UserReportModel>(sql, param);

            var response = result.Skip((param.pageIndex - 1) * param.pageSize).Take(param.pageSize).ToList();

            return Json(new ResponseModel<object>
            {
                RspCode = RspCode.C0000,
                Body = response,
                Page = new PageModel { TotalCount = result.Count(), PageSize = param.pageSize }
            });
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult GetDetail([FromBody]ReportParam param)
        {

            var sql = @"select ua.Answer as UserAnswer,qb.*,u.ID as UserId,u.Name as UserName from UserAnswer ua 
inner join QuestionBank qb on ua.QuestionId=qb.ID
inner join [User] u on u.ID = ua.UserId
where ua.UserId=" + param.id;

            var list = DataAcccessHelper.Query<UserAnswerDetailModel>(sql, param);
            var userInfo = DataAcccessHelper.QueryFirst<User>("select * from [user] where id =" + param.id, param);

            return Json(new ResponseModel<object>
            {
                RspCode = RspCode.C0000,
                Body = new { list, userInfo }
            });
        }


        public JsonResult ExportAll()
        {

            var sql = @" select u.ID as UserId,u.Name as UserName,us.Score,us.CreateTime,us.UpdateTime  FROM [WaterSupplySecurity].[dbo].[UserScore] us inner join [dbo].[User] u on u.ID = us.UserID  ";

            var dt = DbHelperSQL.Query(sql).Tables[0];

            DataTable dtExport = new DataTable();

            dtExport.Columns.Add("用户ID");
            dtExport.Columns.Add("用户姓名");
            dtExport.Columns.Add("分数");
            dtExport.Columns.Add("开始时间");
            dtExport.Columns.Add("结束时间");

            foreach (DataRow d in dt.Rows)
            {
                DataRow drExport = dtExport.NewRow();
                drExport["用户ID"] = d["UserId"];
                drExport["用户姓名"] = d["UserName"];
                drExport["分数"] = d["Score"];
                drExport["开始时间"] = d["CreateTime"].ToString();
                drExport["结束时间"] = d["UpdateTime"].ToString();
                dtExport.Rows.Add(drExport);
            }

            var msg = "";
            ExcelService.DataTableToExcel(dtExport, out msg, "所有分数汇总_" + DateTime.Now.ToString("yyyyMMddHHmmss"));

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoteLogin(string account,string passWord)
        {
            LoginService service = new LoginService();
            var model = service.Login(account, passWord);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemoteIndex(string userId)
        {
            QuestionService service = new QuestionService();
            var model = service.GetQuestionList(userId);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}