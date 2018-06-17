using Microsoft.AspNetCore.Mvc;
using Poetry.UI.RoutingSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.AspNetCore.Controllers
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
            return View($"~/{BasePathProvider.BasePath}/Core/Views/Index.cshtml");
        }
    }
}
