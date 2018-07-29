using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class SubmitAnswerParameter
    {
        public SubmitAnswerParameter()
        {
            UserAnswerList = new List<UserAnswer>();
        }
        public string UserId { get; set; }
        public List<UserAnswer> UserAnswerList { get; set; }
    }
}
