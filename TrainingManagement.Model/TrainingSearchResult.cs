using System.Collections.Generic;

namespace TrainingManagement.Model
{
    public class TrainingSearchResult<T>
    {
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}