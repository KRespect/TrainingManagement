
using SqlSugar;
using System.Collections.Generic;
using TrainingManagement.Model;
using TrainingManagement.Model;
using TrainingManagement.Model;

namespace TrainingManagement.Model
{
    [SugarTable("roles")]
    public class Role
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        [Navigate(NavigateType.OneToMany, nameof(UserRole.RoleId))]
        public List<UserRole> UserRoles { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
}
