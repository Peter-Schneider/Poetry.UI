using Poetry.UI.FormSupport.PropertyDefinitionSupport;
using Poetry.UI.MvcSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.FormSupport.Controllers
{
    [Controller("PropertyDefinition")]
    public class PropertyDefinitionController
    {
        [Action("GetAllFor")]
        public IEnumerable<PropertyDefinition> GetAllFor(string id)
        {
            return new List<PropertyDefinition> {
                new PropertyDefinition(),
            };
        }
    }
}
