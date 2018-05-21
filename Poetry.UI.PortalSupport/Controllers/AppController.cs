using Poetry.UI.AppSupport;
using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poetry.UI.PortalSupport.Controllers
{
    [Controller("App")]
    public class AppController
    {
        IAppRepository AppRepository { get; }

        public AppController(IAppRepository appRepository)
        {
            AppRepository = appRepository;
        }

        [Action("GetNames")]
        public Dictionary<string, string> GetNames()
        {
            return AppRepository.GetAll().ToDictionary(app => app.Name, app => app.Translations.Get("name", "en") ?? app.Name);
        }
    }
}
