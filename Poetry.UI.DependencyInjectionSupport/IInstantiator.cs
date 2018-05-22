using System;

namespace Poetry.UI.DependencyInjectionSupport
{
    public interface IInstantiator
    {
        object Instantiate(Type type);
    }
}
