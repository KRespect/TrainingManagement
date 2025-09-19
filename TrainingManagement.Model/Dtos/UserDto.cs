namespace TrainingManagement.Model.Dtos
{
    /// <summary>
    /// 用户创建DTO
    /// </summary>
    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public int DepartmentId { get; set; }
    }

    /// <summary>
    /// 用户返回DTO
    /// </summary>
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public int DepartmentId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}