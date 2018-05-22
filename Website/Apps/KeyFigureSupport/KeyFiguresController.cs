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
        public static List<KeyFigure> Items { get; } = new List<KeyFigure> {
            new KeyFigure {
                Id = Guid.NewGuid().ToString(),
                Key = "Lorem",
                Value = "Ipsum",
            },
            new KeyFigure {
                Id = Guid.NewGuid().ToString(),
                Key = "Dolor",
                Value = "Sit amet",
            },
            new KeyFigure {
                Id = Guid.NewGuid().ToString(),
                Key = "Adipiscing",
                Value = "Elit",
            },
            new KeyFigure {
                Id = Guid.NewGuid().ToString(),
                Key = "Pellentesque",
                Value = "Cursus",
            },
            new KeyFigure {
                Id = Guid.NewGuid().ToString(),
                Key = "Dictum",
                Value = "Erat",
            }
        };

        [Route("Save")]
        [HttpPost]
        public void Save([FromBody]KeyFigure item)
        {
            if(item.Id == null)
            {
                item.Id = Guid.NewGuid().ToString();
            }

            var index = Items.FindIndex(i => i.Id == item.Id);

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
