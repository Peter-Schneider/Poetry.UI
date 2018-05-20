using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.FormSupport.FormFieldSupport
{
    [Controller("Field")]
    public class FormFieldController
    {
        IFormFieldProvider FormFieldProvider { get; }

        public FormFieldController(IFormFieldProvider formFieldProvider)
        {
            FormFieldProvider = formFieldProvider;
        }

        [Action("GetAllForForm")]
        public IEnumerable<FormField> GetAllForForm(string id)
        {
            return FormFieldProvider.GetAllFor(id);
        }
    }
}
