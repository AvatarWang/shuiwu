using Newtonsoft.Json;
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
        public string Status { get; set; }
        public string NowTime { get; set; }

    }
    public class QuestionEntity
    {
        public string Id { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string Question { get; set; }
        public string Type { get; set; }
        [JsonIgnore]
        public string Answer { get; set; }
        public string Multi { get; set; }
    }
}
