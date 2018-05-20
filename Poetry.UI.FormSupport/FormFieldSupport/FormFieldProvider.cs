using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Poetry.UI.FormSupport.FormFieldSupport
{
    public class FormFieldProvider : IFormFieldProvider
    {
        IDictionary<string, IEnumerable<FormField>> FormFieldsByFormId { get; }

        public FormFieldProvider(Dictionary<string, IEnumerable<FormField>> formFieldsByFormId)
        {
            FormFieldsByFormId = new ReadOnlyDictionary<string, IEnumerable<FormField>>(new Dictionary<string, IEnumerable<FormField>>(formFieldsByFormId));
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
