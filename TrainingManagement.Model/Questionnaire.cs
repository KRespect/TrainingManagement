﻿
using SqlSugar;
using System.Collections.Generic;

namespace TrainingManagement.Model
{
    [SugarTable("questionnaires")]
    public class Questionnaire
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreateTime { get; set; }
        
        public int CreatorId { get; set; }
        
        public int DepartmentId { get; set; }
        
        [Navigate(NavigateType.OneToOne, nameof(DepartmentId))]
        public Department Department { get; set; }
        
        public QuestionnaireStatus Status { get; set; }
        
        [Navigate(NavigateType.OneToMany, nameof(QuestionnaireQuestion.QuestionnaireId))]
        public List<QuestionnaireQuestion> Questions { get; set; }
        

    }
}
