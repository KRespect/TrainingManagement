using SqlSugar;
using TrainingManagement.Model;
using TrainingManagement.Model;

namespace TrainingManagement.Model
{
    [SugarTable("attachments")]
    public class Attachment
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Url { get; set; }
        
        public string FileType { get; set; }
        
        public long FileSize { get; set; }
        
        public int TrainingId { get; set; }
        
        [Navigate(NavigateType.OneToOne, nameof(TrainingId))]
        public Training Training { get; set; }
        
        public int? QuestionnaireId { get; set; }
        
        [Navigate(NavigateType.OneToOne, nameof(QuestionnaireId))]
        public Questionnaire Questionnaire { get; set; }
        
        public DateTime UploadTime { get; set; } = DateTime.Now;
        
        public int UploaderId { get; set; }
        
        [Navigate(NavigateType.OneToOne, nameof(UploaderId))]
        public User Uploader { get; set; }
    }
}