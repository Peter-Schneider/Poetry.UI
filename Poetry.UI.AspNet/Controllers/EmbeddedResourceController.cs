using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Poetry.UI.Controllers
{
    public class EmbeddedResourceController : Controller
    {
        public ActionResult Index(string url)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var path = Path.GetDirectoryName(url).Replace('/', '.').Replace('-', '_');

            if (!string.IsNullOrEmpty(path))
            {
                path += ".";
            }

            var resourceName = $"Poetry.UI.{path}{Path.GetFileName(url)}";
            var mime = MimeMapping.GetMimeMapping(url);

            var stream = assembly.GetManifestResourceStream(resourceName);

            if(stream == null)
            {
                if(Request.Url.Host == "localhost")
                {
                    throw new Exception($"File {resourceName} not found in embedded resources:\n{string.Join("\n", assembly.GetManifestResourceNames())}");
                }

                return HttpNotFound();
            }

            return new FileStreamResult(stream, mime);
        }
    }
}
