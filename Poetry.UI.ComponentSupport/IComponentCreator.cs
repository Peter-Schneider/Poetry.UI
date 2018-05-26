using System;

namespace Poetry.UI.ComponentSupport
{
    public interface IComponentCreator
    {
        Component Create(Type type);
    }
}