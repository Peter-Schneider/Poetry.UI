using Poetry.UI.MvcSupport;
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

        public PoetryPortalController(IBasePathProvider basePathProvider)
        {
            BasePathProvider = basePathProvider;
        }

        public ActionResult Index()
        {
            return View($"~/{BasePathProvider.BasePath}/Views/Index.cshtml");
        }

        public ActionResult StaticFile(string virtualPath)
        {
            virtualPath = $"/{BasePathProvider.BasePath}/{virtualPath}";

            if (!HostingEnvironment.VirtualPathProvider.FileExists(virtualPath))
            {
                return HttpNotFound();
            }

            var file = HostingEnvironment.VirtualPathProvider.GetFile(virtualPath);

            var mimeType = MimeMapping.GetMimeMapping(Path.GetExtension(virtualPath));

            return new FileStreamResult(file.Open(), mimeType);
        }
    }
}
