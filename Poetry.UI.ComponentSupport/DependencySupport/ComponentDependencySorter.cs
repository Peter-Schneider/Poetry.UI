using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poetry.UI.ComponentSupport.DependencySupport
{
    public class ComponentDependencySorter : IComponentDependencySorter
    {
        public IEnumerable<Component> Sort(IEnumerable<Component> components)
        {
            var result = new List<Component>();

            foreach(var component in components)
            {
                var index = result.Count;

                var dependents = components.Where(c => c.Dependencies.Contains(component.Id)).Where(c => result.Contains(c));

                if (dependents.Any())
                {
                    index = dependents.Select(d => result.IndexOf(d)).Min();
                }

                result.Insert(index, component);
            }

            return result;
        }
    }
}
