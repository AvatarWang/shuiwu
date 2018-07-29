using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class UserAnswerDetailModel : QuestionEntity
    {
        public int ID { get; set; }
        public string Answer { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserAnswer { get; set; }
        public int Score { get; set; } = 0;
    }
}
