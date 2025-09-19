namespace TrainingManagement.Model.Dtos
{
    /// <summary>
    /// 问卷创建DTO
    /// </summary>
    public class QuestionnaireCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
    }

    /// <summary>
    /// 问卷返回DTO
    /// </summary>
    public class QuestionnaireDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}