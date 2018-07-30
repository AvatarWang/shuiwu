using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Services
{
    public class DataHelp
    {
         //获取用户信息
        public static User GetUserByAccount(string account)
        {
            User user = new User();
            string sql = string.Format("SELECT [ID] FROM [WaterSupplySecurity].[dbo].[User] where [Account]='{0}'", account);
            user = DataAcccessHelper.QueryFirstOrDefault<User>(sql);
            return user;
        }
        public static bool InsertLoginInfo(string userId)
        {
            string sql = string.Format(@"INSERT INTO [WaterSupplySecurity].[dbo].[UserScore]([UserID],[CreateTime]) VALUES({0},'{1}')",userId,DateTime.Now);
            int reuslt = DataAcccessHelper.Execute(sql.ToString());
            if (reuslt > 0)
            {
                return true;
            }
            return false;
        }
        //获取用户登录提交和得分情况
        public static UserScoreModel GetUserScore(string userId)
        {
            UserScoreModel model = new UserScoreModel();
            string sql = string.Format(@"SELECT 
      [IsSubmit]
      ,[CreateTime]
      ,[Score]
      ,[UpdateTime]
  FROM[WaterSupplySecurity].[dbo].[UserScore] where UserID = {0}",userId);
            model = DataAcccessHelper.QueryFirstOrDefault<UserScoreModel>(sql);
            return model;
        }
        public static List<QuestionEntity> GetQuestionList()
        {
            List<QuestionEntity> list = new List<QuestionEntity>();
            Cache cache = HttpRuntime.Cache;
            if (cache["Keys"] == null)
            {
                string sql = string.Format(@"SELECT ID,
      [Question]
      ,[OptionA]
      ,[OptionB]
      ,[OptionC]
      ,[OptionD]
      ,[Answer]
      ,[Type]
      ,[Multi]
  FROM [WaterSupplySecurity].[dbo].[QuestionBank]");
                list = DataAcccessHelper.Query<QuestionEntity>(sql).ToList();
                cache.Insert("Keys", list);
            }
            {
                list = cache["Keys"] as List<QuestionEntity>;
            }
            return list;
        }
        public static bool SubmitAnswer(SubmitAnswerParameter parameter)
        {
            StringBuilder sql = new StringBuilder();
            foreach (var answer in parameter.UserAnswerList)
            {
                sql.Append(string.Format(@"INSERT INTO [WaterSupplySecurity].[dbo].[UserAnswer]([UserId],[QuestionId],[Answer]) VALUES({0},{1},'{2}');", parameter.UserId,answer.QuestionId,answer.Answer));
            }
            int reuslt = DataAcccessHelper.Execute(sql.ToString());
            if (reuslt > 0)
            {
                return true;
            }
            return false;

        }
        public static bool SubmitScore(string userId, int score)
        {
            //string sql = string.Format(@"INSERT INTO [WaterSupplySecurity].[dbo].[UserScore]([UserID],[IsSubmit],[CreateTime],[Score],[UpdateTime]) VALUES({0},{1},{2},{3},{4})",);
            string sql = string.Format(@"UPDATE [WaterSupplySecurity].[dbo].[UserScore] SET [IsSubmit] = 1,[Score] = {0},[UpdateTime] = '{1}' WHERE UserID={2}",score,DateTime.Now,userId);
            int reuslt = DataAcccessHelper.Execute(sql.ToString());
            if (reuslt > 0)
            {
                return true;
            }
            return false;
        }
    }
}
