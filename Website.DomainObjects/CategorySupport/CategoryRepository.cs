using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Website.CategorySupport
{
    class CategoryRepository : ICategoryRepository
    {
        List<Category> Items { get; } = new List<Category>
        {
            new Category
            {
                Id = "cade3bee-4165-40c5-89ed-6a1622642009",
                Name = "Scooters",
                UrlSegment = "scooters",
            },
            new Category
            {
                Id = "7cba0b8d-9849-4be2-b17c-5e3f749e8c19",
                Name = "Bikes",
                UrlSegment = "bikes",
            },
        };

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