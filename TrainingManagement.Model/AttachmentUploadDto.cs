using Microsoft.AspNetCore.Http;

namespace TrainingManagement.Model
{
    public class AttachmentUploadDto
    {
        /// <summary>
        /// 
        /// </summary>
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public int? TrainingId { get; set; }
        public int? QuestionnaireId { get; set; }
    }
}