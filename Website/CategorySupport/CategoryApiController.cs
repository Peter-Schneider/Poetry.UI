using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Website.CategorySupport
{
    [RoutePrefix("CategorySupport")]
    public class CategoryApiController : ApiController
    {
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
