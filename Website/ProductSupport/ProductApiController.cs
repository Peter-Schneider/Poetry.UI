using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Website.ProductSupport
{
    [RoutePrefix("ProductSupport")]
    public class ProductApiController : ApiController
    {
        [Route("Save")]
        [HttpPost]
        public void Save([FromBody]Product item)
        {
            if(item.Id == null)
            {
                item.Id = Guid.NewGuid().ToString();
            }

            ProductRepository.Save(item);
        }
    }
}
