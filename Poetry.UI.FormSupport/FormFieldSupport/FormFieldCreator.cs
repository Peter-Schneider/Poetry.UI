using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.FormSupport.FormFieldSupport
{
    public class FormFieldCreator : IFormFieldCreator
    {
        public IEnumerable<FormField> Create(Type formType)
        {
            var result = new List<FormField>();

            foreach(var property in formType.GetProperties())
            {
                if (property.GetGetMethod() == null || property.GetSetMethod() == null)
                {
                    continue;
                }

                result.Add(new FormField(property.Name, GetTypeName(property.PropertyType)));
            }

            return result;
        }

        string GetTypeName(Type type)
        {
            if(type == typeof(string))
            {
                return "string";
            }

            if(type == typeof(int))
            {
                return "int";
            }

            if(type == typeof(double))
            {
                return "double";
            }

            if(type == typeof(bool))
            {
                return "boolean";
            }

            return type.Name;
        }
    }
}
