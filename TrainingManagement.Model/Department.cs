﻿
using SqlSugar;
using System.Collections.Generic;

namespace TrainingManagement.Model
{
    
    // Models/Department.cs
    [SugarTable("departments")]
    public class Department
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string Name { get; set; }

        [SugarColumn(Length = 50)]
        public string Code { get; set; }
        
        public int? ParentId { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(Department.ParentId))]
        public List<Department>? Children { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(User.DepartmentId))]
        public List<User> Users { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;

        public Department()
        {
            ParentId = 0;
            CreateTime = DateTime.Now;
        }
    }
}
