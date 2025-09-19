namespace TrainingManagement.Common.Pagination
{
    public class PagedRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}