using System.Collections.Generic;

namespace Website.ProductSupport
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        void Save(Product item);
        Product Get(string id);
    }
}