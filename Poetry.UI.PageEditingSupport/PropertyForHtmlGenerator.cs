using Poetry.UI.FormSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Poetry.UI.PageEditingSupport
{
    public class PropertyForHtmlGenerator : IPropertyForHtmlGenerator
    {
        IObjectIdentifier ObjectIdentifier { get; }
        IPropertyExpressionMetaDataProvider PropertyExpressionMetaDataProvider { get; }
        IFormProvider FormProvider { get; }

        public PropertyForHtmlGenerator(IObjectIdentifier objectIdentifier, IPropertyExpressionMetaDataProvider propertyExpressionMetaDataProvider, IFormProvider formProvider)
        {
            ObjectIdentifier = objectIdentifier;
            PropertyExpressionMetaDataProvider = propertyExpressionMetaDataProvider;
            FormProvider = formProvider;
        }

        public string GenerateHtml(object model, Expression expression, string contents)
        {
            var metaData = PropertyExpressionMetaDataProvider.GetFor(expression);

            var instance = metaData.OwnerObject ?? model;
            var instanceType = instance.GetType();

            var form = FormProvider.GetAll().SingleOrDefault(f => f.Type == instanceType);
            var field = form.Fields.SingleOrDefault(f => f.Id == metaData.PropertyName);

            return 
                $"<span class=\"poetry-page-editing-property\" form-id=\"{form.Id}\" field-type=\"{field.Type}\" property-name=\"{metaData.PropertyName}\" object-id=\"{ObjectIdentifier.GetId(instance)}\">" +
                contents +
                "</span>";
        }
    }
}
