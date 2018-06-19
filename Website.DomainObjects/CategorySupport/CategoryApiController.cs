using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Website.CategorySupport
{
    [Controller("Category")]
    public class CategoryApiController
    {
        ICategoryRepository CategoryRepository { get; }

        public CategoryApiController(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        [Action("Save")]
        public void Save([FromRequestBody]Category item)
        {
            if(item.Id == null)
            {
                item.Id = Guid.NewGuid().ToString();
            }

            CategoryRepository.Save(item);
        }
    }
}
