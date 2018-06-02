using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Poetry.UI.FormSupport;

namespace Website.CategorySupport
{
    [Form("category")]
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UrlSegment { get; set; }
    }
}