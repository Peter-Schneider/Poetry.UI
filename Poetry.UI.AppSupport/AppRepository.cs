using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poetry.UI.AppSupport
{
    public class AppRepository : IAppRepository
    {
        IEnumerable<App> Apps { get; }

        public AppRepository(IAppCreator appCreator)
        {
            Apps = appCreator.Create();
        }

        public IEnumerable<App> GetAll()
        {
            return Apps;
        }
    }
}
