using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserAnswerService
    {
        //用户选择时保存答案的接口
        public SubmitResponseModel SubmitAnswer(SubmitAnswerParameter parameter)
        {
            SubmitResponseModel model = new SubmitResponseModel();
            try
            {
                if (DataHelp.SubmitAnswer(parameter))
                {
                    var questionList = DataHelp.GetQuestionList();
                    int sum = 0;
                    foreach (var answer in parameter.UserAnswerList)
                    {
                        var userAnswer = questionList.Where(x => x.Id == answer.QuestionId).FirstOrDefault();
                        if (userAnswer != null)
                        {
                            if (userAnswer.Answer == answer.Answer)
                            {
                                if (userAnswer.Multi == "1")
                                {
                                    sum++;
                                }
                                else
                                {
                                    sum = sum + 2;
                                }
                            }
                        }
                    }
                    if (DataHelp.SubmitScore(parameter.UserId, sum))
                    {
                        model.Score = sum.ToString();
                        model.Status = "1";
                    }
                    else
                    {
                        model.Status = "0";
                    }
                }
            }
            catch
            {
                model.Status = "0";
            }
            return model;
        }
    }
}
