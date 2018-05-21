using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    public class ComponentRepository : IComponentRepository
    {
        IEnumerable<Component> Components { get; }

        public ComponentRepository(IEnumerable<Component> components)
        {
            Components = components.ToList().AsReadOnly();
        }

        public IEnumerable<Component> GetAll()
        {
            return Components;
        }
    }
}
