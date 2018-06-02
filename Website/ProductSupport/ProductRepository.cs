using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Website.ProductSupport
{
    public static class ProductRepository
    {
        static string BasePath { get { return HostingEnvironment.MapPath(@"/ProductSupport/Data"); } }

        public static IEnumerable<Product> GetAll()
        {
            return Directory.EnumerateFiles(BasePath).Select(f => JsonConvert.DeserializeObject<Product>(File.ReadAllText(f)));
        }

        public static void Save(Product item)
        {
            File.WriteAllText(Path.Combine(BasePath, $"{item.Id}.json"), JsonConvert.SerializeObject(item));
        }

        public static Product Get(string id)
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