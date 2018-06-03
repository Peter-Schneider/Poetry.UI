using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    public class ComponentRepository : IComponentRepository
    {
        IEnumerable<Component> Components { get; }

        public ComponentRepository(IComponentCreator componentCreator)
        {
            Components = componentCreator.Create();
        }

        public IEnumerable<Component> GetAll()
        {
            return Components;
        }
    }
}
