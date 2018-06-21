using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Poetry.UI.FormSupport
{
    public class FormProvider : IFormProvider
    {
        IDictionary<Type, Form> Forms { get; }

        public FormProvider(IFormTypeProvider formTypeProvider, IFormCreator formCreator)
        {
            var forms = new Dictionary<Type, Form>();

            foreach(var type in formTypeProvider.GetAll())
            {
                forms[type] = formCreator.Create(type);
            }

            Forms = new ReadOnlyDictionary<Type, Form>(forms);
        }

        public IEnumerable<Form> GetAll()
        {
            return Forms.Values;
        }
    }
}
