using SqlSugar;
using TrainingManagement.Model;

namespace TrainingManagement.Data
{
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase(ISqlSugarClient db)
        {
            //// 自动创建数据库表
            db.CodeFirst.InitTables(
                typeof(Department),
                typeof(Training),
                typeof(Questionnaire),
                typeof(QuestionnaireQuestion),
                typeof(User),
                typeof(UserRole),
                typeof(Attachment),
                typeof(AnswerRecord),
                typeof(Role),
                typeof(UserRole)

            );

            // 初始化基础数据
            if (!db.Queryable<Department>().Any())
            {
                db.Insertable(insertObj: new Department { 
                    Name = "总公司", 
                    Code = "ROOT",
                    ParentId = 0
                }).ExecuteCommand();
            }

            // 初始化管理员角色
            if (!db.Queryable<Role>().Any())
            {
                var adminRole = new Role
                {
                    Name = "Admin",
                    Description = "系统管理员，拥有所有权限"
                };
                db.Insertable(adminRole).ExecuteCommand();

                // 初始化管理员用户
                var adminUser = new User
                {
                    Name = "管理员",
                    Account = "admin",
                    Avatar="",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"), // 使用BCrypt加密密码
                    DepartmentId = 1,
                    IsActive = true
                };
                db.Insertable(adminUser).ExecuteCommand();

                // 为用户分配角色
                db.Insertable(new UserRole
                {
                    UserId = adminUser.Id,
                    RoleId = adminRole.Id
                }).ExecuteCommand();
            }
        }
    }
}