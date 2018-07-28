using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DataHelp
    {
        public static User GetUserByAccount(string account)
        {
            User user = new User();
            return user;
        }
        public static List<UserAnswer> GetUserAnswer(string userId)
        {
            List<UserAnswer> userAnswerList = new List<UserAnswer>();
            return userAnswerList;
        }
        public static UserScoreModel GetUserScore(string userId)
        {
            UserScoreModel model = new UserScoreModel();
            return model;
        }
    }
}
