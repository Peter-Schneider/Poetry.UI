using Website.ProductSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index(string category, string product)
        {
            var item = ProductRepository.GetAll().FirstOrDefault(p => p.ArticleNo == product);

            return View(item);
        }
    }
}