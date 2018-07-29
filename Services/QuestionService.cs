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
                var questionList = DataHelp.GetQuestionList();
                var oneList = questionList.Where(x => x.Type == "1"&&x.Multi=="0").Take(8).ToList();
                var oneMultiList = questionList.Where(x => x.Type == "1" && x.Multi == "1").Take(2).ToList();
                int type = 6;
                int multi = 2;
                for (int i = 1; i < type; i++)
                {
                    for (int j = 0; j < multi; j++)
                    {
                        if (j == 0)
                        {
                            model.QuestionList.AddRange(questionList.Where(x => x.Type == i.ToString() && x.Multi == j.ToString()).Take(8).ToList());
                        }
                        else
                        {
                            model.QuestionList.AddRange(questionList.Where(x => x.Type == i.ToString() && x.Multi == j.ToString()).Take(2).ToList());
                        }
                    }
                }
                model.IsSubmit = "0";

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
