using Website.CategorySupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryRepository CategoryRepository { get; }

        public CategoryController(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        public ActionResult Index(string category)
        {
            var item = CategoryRepository.GetAll().FirstOrDefault(c => c.UrlSegment == category);

            return View(item);
        }
    }
}