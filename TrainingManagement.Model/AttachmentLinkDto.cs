namespace TrainingManagement.Model
{
    public class AttachmentLinkDto
    {
        public int AttachmentId { get; set; }
        public int? TrainingId { get; set; }
        public int? QuestionnaireId { get; set; }
    }
}