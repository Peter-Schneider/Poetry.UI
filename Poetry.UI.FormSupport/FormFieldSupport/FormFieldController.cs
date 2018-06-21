using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poetry.UI.FormSupport.FormFieldSupport
{
    [Controller("Field")]
    public class FormFieldController
    {
        IFormProvider FormProvider { get; }

        public FormFieldController(IFormProvider formFieldProvider)
        {
            FormProvider = formFieldProvider;
        }

        [Action("GetAllForForm")]
        public IEnumerable<FormField> GetAllForForm(string id)
        {
            var form = FormProvider.GetAll().SingleOrDefault(f => f.Id == id);

            if(form == null)
            {
                return null;
            }

            return form.Fields;
        }
    }
}
