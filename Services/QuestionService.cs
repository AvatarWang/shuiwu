using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class QuestionService
    {
        public QuestionModel GetQuestionList(string userId)
        {
            QuestionModel model = new QuestionModel();
            var userScore = DataHelp.GetUserScore(userId);
            if (userScore.IsSubmit != "1")
            {
                var userAnswerList = DataHelp.GetUserAnswer(userId);
                //已经有数据的情况下根据题目id去题库读取数据返回
                if (userAnswerList.Any())
                {
                    
                }
                else
                {
                    //无数据的情况下根据规则拿去数据
                }
            }
            else
            {
                //已经提交了，直接给分
                model.IsSubmit = "1";
                model.Score = userScore.Score;
            }
            return model;
        }
    }
}
