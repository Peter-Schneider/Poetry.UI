using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.FormSupport.FormFieldSupport
{
    public class FormField
    {
        public string Id { get; }
        public string Type { get; }
        public bool AutoGenerate { get; }

        public FormField(string id, string type, bool autoGenerate)
        {
            Id = id;
            Type = type;
            AutoGenerate = autoGenerate;
        }
    }
}
