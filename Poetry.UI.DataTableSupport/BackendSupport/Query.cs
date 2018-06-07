namespace Poetry.UI.DataTableSupport.BackendSupport
{
    public class Query
    {
        public int Page { get; }
        public string SortBy { get; }

        public Query(int page, string sortBy)
        {
            Page = page;
            SortBy = sortBy;
        }
    }
}