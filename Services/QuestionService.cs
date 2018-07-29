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
                List<QuestionEntity> singleQuestionList = new List<QuestionEntity>();
                List<QuestionEntity> multiQuestionList = new List<QuestionEntity>();
                for (int j = 0; j < multi; j++)
                {
                    for (int i = 1; i < type; i++)
                    {

                        if (j == 0)
                        {
                            singleQuestionList.AddRange(questionList.Where(x => x.Type == i.ToString() && x.Multi == j.ToString()).Take(8).ToList());
                        }
                        else
                        {
                            multiQuestionList.AddRange(questionList.Where(x => x.Type == i.ToString() && x.Multi == j.ToString()).Take(2).ToList());
                        }
                    }
                }
                model.QuestionList.AddRange(singleQuestionList.OrderBy(x => Guid.NewGuid()));
                model.QuestionList.AddRange(multiQuestionList.OrderBy(x => Guid.NewGuid()));
                model.IsSubmit = "0";
                model.FirstLoginTime = userScore.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

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
