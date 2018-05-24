using Poetry.UI.PageEditingSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI.WebControls;

namespace Poetry.UI.AspNet.PageEditingSupport
{
    public static class TemplateHelpers
    {
        static IModeProvider ModeProvider { get; set; }

        public static MvcHtmlString PropertyFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            if(ModeProvider == null)
            {
                ModeProvider = DependencyResolver.Current.GetService<IModeProvider>();
            }

            var result = html.DisplayFor(expression);

            if(ModeProvider.GetIsPageEditing(html.ViewContext.HttpContext) != true)
            {
                return result;
            }

            return MvcHtmlString.Create($"<span class=\"poetry-page-editing-property\">{result}</span>");
        }
    }
}
