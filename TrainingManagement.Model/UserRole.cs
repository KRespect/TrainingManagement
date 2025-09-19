using SqlSugar;

namespace TrainingManagement.Model
{
    [SugarTable("user_roles")]
    public class UserRole
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int UserId { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public int RoleId { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(UserId))]
        public User User { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(RoleId))]
        public Role Role { get; set; }
    }
}