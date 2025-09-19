using System;

namespace TrainingManagement.Model
{
    public class TrainingSearchRequest
    {
        public string Name { get; set; }
        public bool? IsPublished { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}