using Poetry.UI.ReflectionSupport;
using System.Reflection;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public interface IEmbeddedResourceAssemblyCreator
    {
        EmbeddedResourceAssembly Create(string basePath, AssemblyWrapper assembly);
    }
}