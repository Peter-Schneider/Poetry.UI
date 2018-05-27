using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Poetry.UI.PageEditingSupport
{
    public interface IPropertyExpressionMetaDataProvider
    {
        PropertyExpressionMetaData GetFor(Expression expression);
    }
}
