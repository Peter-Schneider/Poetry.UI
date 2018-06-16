using Poetry.UI.DependencyInjectionSupport;
using Poetry.UI.FormSupport.FormFieldSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.FormSupport
{
    [DependencyInjector]
    public class FormSupportDependencyInjector : IDependencyInjector
    {
        public void InjectDependencies(IContainer container)
        {
            container.RegisterType<IFormTypeProvider, FormTypeProvider>();
            container.RegisterType<IFormFieldCreator, FormFieldCreator>();
            container.RegisterType<IFormCreator, FormCreator>();
            container.RegisterType<IFormFieldProvider, FormFieldProvider>();
        }
    }
}
