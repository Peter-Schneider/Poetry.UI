namespace Poetry.UI.EmbeddedResourceSupport
{
    public interface IEmbeddedResourcePathMatcher
    {
        bool Match(string path, EmbeddedResource embeddedResource);
    }
}