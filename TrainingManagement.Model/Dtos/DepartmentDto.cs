namespace TrainingManagement.Model.Dtos
{
    /// <summary>
    /// 部门创建DTO
    /// </summary>
    public class DepartmentCreateDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int? ParentId { get; set; }
    }

    /// <summary>
    /// 部门返回DTO
    /// </summary>
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreateTime { get; set; }
        public List<DepartmentDto> Children { get; set; }
    }

    /// <summary>
    /// 部门树形结构DTO
    /// </summary>
    public class DepartmentTreeDto : DepartmentDto
    {
        public List<DepartmentTreeDto> Children { get; set; }
    }
}