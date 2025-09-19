
using SqlSugar;
using TrainingManagement.Model;
using System.Collections.Generic;

namespace TrainingManagement.Model
{

    // 
    [SugarTable("answer_records")]
    public class AnswerRecord
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public int UserId { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(UserId))]
        public User User { get; set; }

        public int QuestionnaireId { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(QuestionnaireId))]
        public Questionnaire Questionnaire { get; set; }

        [SugarColumn(IsJson = true)]
        public List<AnswerDetail> Answers { get; set; }

        public decimal TotalScore { get; set; }

        public DateTime AnswerTime { get; set; } = DateTime.Now;
    }
    public class AnswerDetail
    {
        public int QuestionId { get; set; }
        public List<string> UserAnswers { get; set; }
        public bool IsCorrect { get; set; }
        public decimal Score { get; set; }
    }

}
