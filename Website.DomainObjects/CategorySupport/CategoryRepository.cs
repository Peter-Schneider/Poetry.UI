using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Website.CategorySupport
{
    class CategoryRepository : ICategoryRepository
    {
        List<Category> Items { get; } = new List<Category>();

        public IEnumerable<Category> GetAll()
        {
            return Items.ToList();
        }

        public void Save(Category item)
        {
            var existing = Items.SingleOrDefault(i => i.Id == item.Id);

            if (existing != null)
            {
                Items.Remove(existing);
            }

            Items.Add(item);
        }

        public Category Get(string id)
        {
            return Items.SingleOrDefault(i => i.Id == id);
        }
    }
}