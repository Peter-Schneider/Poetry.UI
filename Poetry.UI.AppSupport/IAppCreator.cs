using System.Collections.Generic;

namespace Poetry.UI.AppSupport
{
    public interface IAppCreator
    {
        IEnumerable<App> Create();
    }
}