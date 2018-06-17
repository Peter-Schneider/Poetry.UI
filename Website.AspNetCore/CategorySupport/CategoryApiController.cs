using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Website.CategorySupport
{
    [Route("CategorySupport")]
    public class CategoryApiController : Controller
    {
        CategoryRepository CategoryRepository { get; }

        public CategoryApiController(CategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        [Route("Save")]
        [HttpPost]
        public void Save([FromBody]Category item)
        {
            if(item.Id == null)
            {
                item.Id = Guid.NewGuid().ToString();
            }

            CategoryRepository.Save(item);
        }
    }
}
