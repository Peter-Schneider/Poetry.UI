using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Apps.KeyFigureSupport;

namespace Website.Controllers
{
    public class KeyFigureController : Controller
    {
        public ActionResult Index(string id)
        {
            var item = KeyFiguresController.Items.SingleOrDefault(i => i.Id == id);

            return View(item);
        }
    }
}