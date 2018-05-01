namespace Poetry.UI.AppSupport
{
    public class Script
    {
        public string Src { get; }
        public int Order { get; }

        public Script(string src, int order)
        {
            Src = src;
            Order = order;
        }
    }
}