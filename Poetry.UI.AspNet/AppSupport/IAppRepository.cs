using System.Collections.Generic;

namespace Poetry.UI.AppSupport
{
    public interface IAppRepository
    {
        IEnumerable<App> GetAll();
    }
}