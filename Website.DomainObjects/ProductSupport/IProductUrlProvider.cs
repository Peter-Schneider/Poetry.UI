using System;
using System.Collections.Generic;
using System.Text;

namespace Website.ProductSupport
{
    public interface IProductUrlProvider
    {
        string GetUrl(Product product);
    }
}
