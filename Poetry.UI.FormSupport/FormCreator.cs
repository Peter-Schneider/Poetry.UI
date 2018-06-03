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
        IFormTypeProvider FormTypeProvider { get; }

        public FormCreator(IFormFieldCreator formFieldCreator, IFormTypeProvider formTypeProvider)
        {
            FormFieldCreator = formFieldCreator;
            FormTypeProvider = formTypeProvider;
        }

        public IEnumerable<Form> Create()
        {
            var result = new List<Form>();

            foreach(var type in FormTypeProvider.GetTypes())
            {
                var attribute = type.GetCustomAttribute<FormAttribute>();

                result.Add(new Form(attribute.Id, FormFieldCreator.Create(type)));
            }

            return result;
        }
    }
}
