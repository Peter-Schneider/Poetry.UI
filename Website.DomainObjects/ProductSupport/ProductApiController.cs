using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Website.ProductSupport
{
    [Controller("Product")]
    public class ProductApiController
    {
        IProductRepository ProductRepository { get; }

        public ProductApiController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        [Action("Save")]
        public void Save([FromRequestBody]Product item)
        {
            if(item.Id == null)
            {
                item.Id = Guid.NewGuid().ToString();
            }

            ProductRepository.Save(item);
        }
    }
}
