using System.Linq.Expressions;

namespace Poetry.UI.PageEditingSupport
{
    public interface IPropertyForHtmlGenerator
    {
        string GenerateHtml(object model, Expression expression, string contents);
    }
}