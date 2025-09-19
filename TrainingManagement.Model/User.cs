﻿
using SqlSugar;

namespace TrainingManagement.Model
{

    [SugarTable("users")]
    public class User
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string? Avatar { get; set; }
        /// <summary>
        /// 性别1:男, 2:女
        /// </summary>
        public int? Gender { get; set; } = 0;

        public string? Phone { get; set; }

        public int DepartmentId { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(DepartmentId))]
        public Department Department { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public string? Education { get; set; } 

        /// <summary>
        /// 专业
        /// </summary>
        public string? Major { get; set; }
        /// <summary>
        /// 电子签名路径
        /// </summary>
        public string? Signature { get; set; } // 电子签名路径
        /// <summary>
        /// 账号
        /// </summary>
        public string? Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>

        public string? Password { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>

        public bool IsActive { get; set; } = true;

        [Navigate(NavigateType.ManyToMany, nameof(UserRole))]
        public List<Role>? Roles { get; set; }
    }

}
