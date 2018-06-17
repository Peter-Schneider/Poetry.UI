using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Website.ProductSupport
{
    public class ProductRepository
    {
        string BasePath { get; }

        public ProductRepository(IHostingEnvironment hostingEnvironment)
        {
            BasePath = Path.Combine(hostingEnvironment.ContentRootPath, @"ProductSupport/Data");
        }

        public IEnumerable<Product> GetAll()
        {
            return Directory.EnumerateFiles(BasePath).Select(f => JsonConvert.DeserializeObject<Product>(File.ReadAllText(f)));
        }

        public void Save(Product item)
        {
            File.WriteAllText(Path.Combine(BasePath, $"{item.Id}.json"), JsonConvert.SerializeObject(item));
        }

        public Product Get(string id)
        {
            var path = Path.Combine(BasePath, $"{id}.json");

            if (!File.Exists(path))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Product>(File.ReadAllText(path));
        }
    }
}