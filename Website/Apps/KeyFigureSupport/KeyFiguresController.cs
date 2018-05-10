using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Website.Apps.KeyFigureSupport
{
    [RoutePrefix("Apps/KeyFigures")]
    public class KeyFiguresController : ApiController
    {
        List<KeyFigure> Items { get; } = new List<KeyFigure> {
            new KeyFigure {
                Key = "Lorem",
                Value = "Ipsum",
            }
        };

        [Route("GetAll")]
        public IEnumerable<KeyFigure> GetAll()
        {
            return Items;
        }

        [Route("Save")]
        [HttpPost]
        public void Save([FromBody]KeyFigure item)
        {
            var index = Items.FindIndex(i => i.Key == item.Key);

            if (index != -1)
            {
                Items[index] = item;
            }
            else
            {
                Items.Add(item);
            }
        }
    }
}
