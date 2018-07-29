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
            try
            {
                var userScore = DataHelp.GetUserScore(userId);
                if (userScore.IsSubmit != "1")
                {
                    var questionList = DataHelp.GetQuestionList();
                    int type = 6;
                    int multi = 3;
                    List<QuestionEntity> singleQuestionList = new List<QuestionEntity>();
                    List<QuestionEntity> multiQuestionList = new List<QuestionEntity>();
                    for (int j = 1; j < multi; j++)
                    {
                        for (int i = 1; i < type; i++)
                        {

                            if (j == 1)
                            {
                                singleQuestionList.AddRange(questionList.Where(x => x.Type == i.ToString() && x.Multi == j.ToString()).OrderBy(x => Guid.NewGuid()).Take(8).ToList());
                            }
                            else
                            {
                                multiQuestionList.AddRange(questionList.Where(x => x.Type == i.ToString() && x.Multi == j.ToString()).OrderBy(x => Guid.NewGuid()).Take(2).ToList());
                            }
                        }
                    }
                    model.QuestionList.AddRange(singleQuestionList.OrderBy(x => Guid.NewGuid()));
                    model.QuestionList.AddRange(multiQuestionList.OrderBy(x => Guid.NewGuid()));
                    model.IsSubmit = "0";
                    model.FirstLoginTime = ((userScore.CreateTime.ToUniversalTime().Ticks - 621355968000000000)/ 10000000).ToString();
                    model.NowTime = ((DateTime.Now.ToUniversalTime().Ticks- 621355968000000000)/10000000).ToString();

                }
                else
                {
                    //已经提交了，直接给分
                    model.IsSubmit = "1";
                    model.Score = userScore.Score;
                }
                model.Status = "1";
            }
            catch
            {
                model.Status = "0";
            }
            return model;
        }
    }
}
