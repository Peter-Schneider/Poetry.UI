using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Website.CategorySupport
{
    public static class CategoryRepository
    {
        static string BasePath { get { return HostingEnvironment.MapPath(@"/CategorySupport/Data"); } }

        public static IEnumerable<Category> GetAll()
        {
            return Directory.EnumerateFiles(BasePath).Select(f => JsonConvert.DeserializeObject<Category>(File.ReadAllText(f)));
        }

        public static void Save(Category item)
        {
            File.WriteAllText(Path.Combine(BasePath, $"{item.Id}.json"), JsonConvert.SerializeObject(item));
        }

        public static Category Get(string id)
        {
            var path = Path.Combine(BasePath, $"{id}.json");

            if (!File.Exists(path))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Category>(File.ReadAllText(path));
        }
    }
}