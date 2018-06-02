using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Poetry.UI.FormSupport;

namespace Website.ProductSupport
{
    [Form("product")]
    public class Product
    {
        public string Id { get; set; }
        public string ArticleNo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public double Price { get; set; }
    }
}