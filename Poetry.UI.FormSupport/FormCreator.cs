using Poetry.UI.FormSupport.FormFieldSupport;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Poetry.UI.FormSupport
{
    public class FormCreator
    {
        IFormFieldCreator FormFieldCreator { get; }

        public FormCreator(IFormFieldCreator formFieldCreator)
        {
            FormFieldCreator = formFieldCreator;
        }

        public IEnumerable<Form> Create(params Type[] types)
        {
            var result = new List<Form>();

            foreach(var type in types)
            {
                var attribute = type.GetCustomAttribute<FormAttribute>();

                result.Add(new Form(attribute.Id, FormFieldCreator.Create(type)));
            }

            return result;
        }
    }
}
