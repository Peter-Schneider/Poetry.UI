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
            container.RegisterSingleton<IFormTypeProvider, FormTypeProvider>();
            container.RegisterSingleton<IFormFieldCreator, FormFieldCreator>();
            container.RegisterSingleton<IFormCreator, FormCreator>();
            container.RegisterSingleton<IFormProvider, FormProvider>();
        }
    }
}
