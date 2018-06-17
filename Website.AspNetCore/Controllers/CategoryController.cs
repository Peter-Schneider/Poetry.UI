using Website.CategorySupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository CategoryRepository { get; }

        public CategoryController(CategoryRepository categoryRepository)
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