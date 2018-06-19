using System;
using System.Collections.Generic;
using System.Text;

namespace Website.CategorySupport
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        void Save(Category item);
        Category Get(string id);
    }
}
