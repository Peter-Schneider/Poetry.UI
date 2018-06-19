using System;
using System.Collections.Generic;
using System.Text;

namespace Website.CategorySupport
{
    public interface ICategoryUrlProvider
    {
        string GetUrl(Category category);
    }
}
