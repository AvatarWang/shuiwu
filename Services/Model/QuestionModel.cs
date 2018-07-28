using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class QuestionModel
    {
        public QuestionModel()
        {
            QuestionList = new List<QuestionEntity>();
        }
        public string FirstLoginTime { get; set; }
        public string IsSubmit { get; set; }
        public string Score { get; set; }
        public List<QuestionEntity> QuestionList { get; set; }

    }
    public class QuestionEntity
    {
        public string QuestionId { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string UserAnswer { get; set; }
    }
}
