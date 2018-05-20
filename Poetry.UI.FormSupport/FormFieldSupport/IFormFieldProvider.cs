using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.FormSupport.FormFieldSupport
{
    public interface IFormFieldProvider
    {
        IEnumerable<FormField> GetAllFor(string formId);
    }
}
