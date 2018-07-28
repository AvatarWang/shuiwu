using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class UserAnswer
    {
        public UserAnswer()
        {
            AnswerList = new List<string>();
        }
        public string QuestionId { get; set; }
        public string Answer { get; set; }
        public List<string> AnswerList { get; set; }
    }
}
