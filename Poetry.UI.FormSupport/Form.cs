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
        public Type Type { get; }
        public IEnumerable<FormField> Fields { get; }

        public Form(string id, Type type, IEnumerable<FormField> fields)
        {
            Id = id;
            Type = type;
            Fields = fields.ToList().AsReadOnly();
        }
    }
}
