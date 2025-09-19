
using SqlSugar;
using System.Collections.Generic;
using TrainingManagement.Model;
using TrainingManagement.Model;

namespace TrainingManagement.Model
{
    [SugarTable("questionnaire_questions")]
    public class QuestionnaireQuestion
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        
        public int QuestionnaireId { get; set; }
        
        public int QuestionId { get; set; }
        
        public int SortOrder { get; set; }
        
        [Navigate(NavigateType.OneToOne, nameof(QuestionnaireId))]
        public Questionnaire Questionnaire { get; set; }
        
        [Navigate(NavigateType.OneToOne, nameof(QuestionId))]
        public Question Question { get; set; }
    }
}
