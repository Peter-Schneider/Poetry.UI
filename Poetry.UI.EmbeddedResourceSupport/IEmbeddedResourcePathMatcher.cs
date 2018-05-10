namespace Poetry.UI.EmbeddedResourceSupport
{
    public interface IEmbeddedResourcePathMatcher
    {
        bool Match(EmbeddedResourceAssembly embeddedResourceAssembly, EmbeddedResource embeddedResource, string path);
    }
}