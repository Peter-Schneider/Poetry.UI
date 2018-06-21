using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Poetry.UI.FormSupport.FormFieldSupport
{
    public class FormFieldProvider : IFormFieldProvider
    {
        IDictionary<string, IEnumerable<FormField>> FormFieldsByFormId { get; }

        public FormFieldProvider(IFormTypeProvider formTypeProvider, IFormCreator formCreator)
        {
            FormFieldsByFormId = new ReadOnlyDictionary<string, IEnumerable<FormField>>(formTypeProvider.GetTypes().Select(t => formCreator.Create(t)).ToDictionary(f => f.Id, f => f.Fields));
        }

        public IEnumerable<FormField> GetAllFor(string formId)
        {
            if (!FormFieldsByFormId.ContainsKey(formId))
            {
                return null;
            }

            return FormFieldsByFormId[formId];
        }
    }
}
