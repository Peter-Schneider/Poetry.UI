using Poetry.UI.FormSupport.FormFieldSupport;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Poetry.UI.FormSupport
{
    public class FormCreator : IFormCreator
    {
        IFormFieldCreator FormFieldCreator { get; }

        public FormCreator(IFormFieldCreator formFieldCreator)
        {
            FormFieldCreator = formFieldCreator;
        }

        public Form Create(Type type)
        {
            var attribute = type.GetCustomAttribute<FormAttribute>();

            return new Form(attribute.Id, type, FormFieldCreator.Create(type));
        }
    }
}
