using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Poetry.UI.PageEditingSupport
{
    public class PropertyExpressionMetaDataProvider : IPropertyExpressionMetaDataProvider
    {
        public PropertyExpressionMetaData GetFor(Expression expression)
        {
            var memberExpression = GetMemberExpression(expression);

            if (memberExpression == null)
            {
                throw new UnsupportedPropertyExpression("You have used an unsupported way to access your property. The only way to access a property is to directly access it through an identifiable object. (like: instance => instance.MyPublicProperty)");
            }

            if(memberExpression.Expression.NodeType != ExpressionType.Parameter)
            {
                throw new UnsupportedPropertyExpression("You have used an unsupported way to supply your object. The only way to access a property is to directly access it through the function parameter. (like: instance => instance.MyPublicProperty)");
            }

            return new PropertyExpressionMetaData(memberExpression.Member.Name, null);
        }

        MemberExpression GetMemberExpression(Expression expression)
        {
            var lambdaExpression = expression as LambdaExpression;

            if (lambdaExpression == null)
            {
                return null;
            }

            var memberExpression = lambdaExpression.Body as MemberExpression;

            if (memberExpression == null)
            {
                var body = (UnaryExpression)lambdaExpression.Body;
                memberExpression = body.Operand as MemberExpression;
            }

            return memberExpression;
        }
    }
}
