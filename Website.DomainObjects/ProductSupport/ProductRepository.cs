using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Website.ProductSupport
{
    class ProductRepository : IProductRepository
    {
        List<Product> Items { get; } = new List<Product>();

        public IEnumerable<Product> GetAll()
        {
            return Items.ToList();
        }

        public void Save(Product item)
        {
            var existing = Items.SingleOrDefault(i => i.Id == item.Id);

            if (existing != null)
            {
                Items.Remove(existing);
            }

            Items.Add(item);
        }

        public Product Get(string id)
        {
            return Items.SingleOrDefault(i => i.Id == id);
        }
    }
}