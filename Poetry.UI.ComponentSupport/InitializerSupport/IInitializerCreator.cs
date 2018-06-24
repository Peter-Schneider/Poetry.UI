using System;

namespace Poetry.UI.ComponentSupport.InitializerSupport
{
    public interface IInitializerCreator
    {
        IInitializer Create(Type type);
    }
}