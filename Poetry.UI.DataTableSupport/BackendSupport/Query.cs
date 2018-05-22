namespace Poetry.UI.DataTableSupport.BackendSupport
{
    public class Query
    {
        public int Page { get; }

        public Query(int page)
        {
            Page = page;
        }
    }
}