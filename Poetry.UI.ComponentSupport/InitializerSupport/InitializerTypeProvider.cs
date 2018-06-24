using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.ComponentSupport.InitializerSupport
{
    public class InitializerTypeProvider : IInitializerTypeProvider
    {
        IComponentRepository ComponentRepository { get; }

        public InitializerTypeProvider(IComponentRepository componentRepository)
        {
            ComponentRepository = componentRepository;
        }

        public IEnumerable<Type> GetAll()
        {
            return ComponentRepository.GetAll().Select(c => c.Assembly).SelectMany(a => a.Types).Where(t => t.GetCustomAttribute<InitializerAttribute>() != null);
        }
    }
}
