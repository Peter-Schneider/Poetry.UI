using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.FormSupport.FormFieldSupport
{
    public class FormField
    {
        public string Id { get; }
        public string Type { get; }

        public FormField(string id, string type)
        {
            Id = id;
            Type = type;
        }
    }
}
