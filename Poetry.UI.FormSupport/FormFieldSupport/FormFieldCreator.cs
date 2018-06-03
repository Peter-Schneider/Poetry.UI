using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
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

                var display = property.GetCustomAttribute<DisplayAttribute>();

                var autoGenerate = display?.AutoGenerateField ?? true;

                result.Add(new FormField(property.Name, GetTypeName(property.PropertyType), autoGenerate));
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
