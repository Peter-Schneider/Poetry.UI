using System.Reflection;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public interface IEmbeddedResourceAssemblyCreator
    {
        EmbeddedResourceAssembly Create(string basePath, Assembly assembly);
    }
}