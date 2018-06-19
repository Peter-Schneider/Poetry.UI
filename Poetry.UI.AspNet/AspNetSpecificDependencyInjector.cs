using Poetry.UI.AspNet.FileSupport;
using Poetry.UI.AspNet.PageEditingSupport;
using Poetry.UI.DependencyInjectionSupport;
using Poetry.UI.FileSupport;
using Poetry.UI.PageEditingSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poetry.UI.AspNet
{
    public class AspNetSpecificDependencyInjector : IDependencyInjector
    {
        public void InjectDependencies(IContainer container)
        {
            container.RegisterSingleton<IFileProvider, FileProvider>();
            container.RegisterSingleton<IModeProvider, ModeProvider>();
        }
    }
}
