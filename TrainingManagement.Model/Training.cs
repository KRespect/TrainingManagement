﻿﻿﻿
using SqlSugar;
using System;
using System.Collections.Generic;

namespace TrainingManagement.Model
{

    // Models/Training.cs
    [SugarTable("trainings")]
    public class Training
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPublished { get; set; }

        public int DepartmentId { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(DepartmentId))]
        public Department Department { get; set; }

        public TrainingStatus Status { get; set; }
    

        /// <summary>
        /// 视频URL
        /// </summary>
        public string VideoUrl { get; set; }
        [Navigate(NavigateType.OneToMany, nameof(Attachment.TrainingId))]
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

        public int CreatorId { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(CreatorId))]
        public User Creator { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime? PublishTime { get; set; }
    }
}
