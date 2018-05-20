using Poetry.UI.FormSupport.FormFieldSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poetry.UI.FormSupport
{
    public class Form
    {
        public string Id { get; }
        public IEnumerable<FormField> Fields { get; }

        public Form(string id, IEnumerable<FormField> fields)
        {
            Id = id;
            Fields = fields.ToList().AsReadOnly();
        }
    }
}
