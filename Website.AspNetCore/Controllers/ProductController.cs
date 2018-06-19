using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Website.ProductSupport;

namespace Website.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository ProductRepository { get; }

        public ProductController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        public ActionResult Index(string category, string product)
        {
            var item = ProductRepository.GetAll().FirstOrDefault(p => p.ArticleNo == product);

            return View(item);
        }
    }
}