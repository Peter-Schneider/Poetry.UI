using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Poetry.UI.PageEditingSupport
{
    public class PropertyForHtmlGenerator : IPropertyForHtmlGenerator
    {
        IObjectIdentifier ObjectIdentifier { get; }
        IPropertyExpressionMetaDataProvider PropertyExpressionMetaDataProvider { get; }

        public PropertyForHtmlGenerator(IObjectIdentifier objectIdentifier, IPropertyExpressionMetaDataProvider propertyExpressionMetaDataProvider)
        {
            ObjectIdentifier = objectIdentifier;
            PropertyExpressionMetaDataProvider = propertyExpressionMetaDataProvider;
        }

        public string GenerateHtml(object model, Expression expression, string contents)
        {
            var metaData = PropertyExpressionMetaDataProvider.GetFor(expression);

            return 
                $"<span class=\"poetry-page-editing-property\" property-name=\"{metaData.PropertyName}\" object-id=\"{ObjectIdentifier.GetId(metaData.OwnerObject ?? model)}\">" +
                contents +
                "</span>";
        }
    }
}
