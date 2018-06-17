using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Website.CategorySupport
{
    public class CategoryRepository
    {
        string BasePath { get; }

        public CategoryRepository(IHostingEnvironment hostingEnvironment)
        {
            BasePath = Path.Combine(hostingEnvironment.ContentRootPath, @"CategorySupport/Data");
        }

        public IEnumerable<Category> GetAll()
        {
            return Directory.EnumerateFiles(BasePath).Select(f => JsonConvert.DeserializeObject<Category>(File.ReadAllText(f)));
        }

        public void Save(Category item)
        {
            File.WriteAllText(Path.Combine(BasePath, $"{item.Id}.json"), JsonConvert.SerializeObject(item));
        }

        public Category Get(string id)
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