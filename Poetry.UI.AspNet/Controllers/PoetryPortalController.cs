using Poetry.UI.EmbeddedResourceSupport;
using Poetry.UI.RoutingSupport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Poetry.UI.Controllers
{
    public class PoetryPortalController : Controller
    {
        IBasePathProvider BasePathProvider { get; }
        EmbeddedResourceVirtualPathViewProvider EmbeddedResourceVirtualPathProvider { get; }

        public PoetryPortalController(IBasePathProvider basePathProvider)
        {
            BasePathProvider = basePathProvider;
        }

        public ActionResult Index()
        {
            return View($"~/{BasePathProvider.BasePath}/Core/Views/Index.cshtml");
        }
    }
}
