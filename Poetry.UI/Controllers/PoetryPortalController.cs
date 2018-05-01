using Poetry.UI.MvcSupport;
using Poetry.UI.EmbeddedResourceSupport;
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
        EmbeddedResourceVirtualPathProvider EmbeddedResourceVirtualPathProvider { get; }

        public PoetryPortalController(IBasePathProvider basePathProvider, EmbeddedResourceVirtualPathProvider embeddedResourceVirtualPathProvider)
        {
            BasePathProvider = basePathProvider;
            EmbeddedResourceVirtualPathProvider = embeddedResourceVirtualPathProvider;
        }

        public ActionResult Index(string url = null)
        {
            if(url != null)
            {
                var virtualPath = $"/{BasePathProvider.BasePath}/{url}";

                if (!EmbeddedResourceVirtualPathProvider.FileExists(virtualPath))
                {
                    return HttpNotFound();
                }

                var file = EmbeddedResourceVirtualPathProvider.GetFile(virtualPath);

                var mimeType = MimeMapping.GetMimeMapping(Path.GetExtension(virtualPath));

                return new FileStreamResult(file.Open(), mimeType);
            }

            return View($"~/{BasePathProvider.BasePath}/Views/Index.cshtml");
        }

        public ActionResult Test()
        {
            return Content("hej");
        }

        public ActionResult StaticFile()
        {
            var url = HttpContext.Request.Url.LocalPath;

           

            //virtualPath = $"/{BasePathProvider.BasePath}/{virtualPath}";

            if (!EmbeddedResourceVirtualPathProvider.FileExists(url))
            {
                return HttpNotFound();
            }

            var file = EmbeddedResourceVirtualPathProvider.GetFile(url);

            var mimeType = MimeMapping.GetMimeMapping(Path.GetExtension(url));

            return new FileStreamResult(file.Open(), mimeType);
        }
    }
}
