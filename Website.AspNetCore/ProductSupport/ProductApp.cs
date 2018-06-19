using Poetry.UI.AppSupport;
using Poetry.UI.ScriptSupport;
using Poetry.UI.TranslationSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.ProductSupport
{
    [App("Product")]
    [Script("/ProductSupport/Scripts/product.js")]
    [Translations("Website/ProductSupport/translations.xml")]
    public class ProductApp
    {
    }
}