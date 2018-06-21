using Poetry.UI.PageEditingSupport;
using Poetry.UI.RoutingSupport;
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
    public static class HtmlHelperExtensions
    {
        static IModeProvider ModeProvider { get; set; }
        static IPropertyForHtmlGenerator PropertyForHtmlGenerator { get; set; }

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

            if (PropertyForHtmlGenerator == null)
            {
                PropertyForHtmlGenerator = DependencyResolver.Current.GetService<IPropertyForHtmlGenerator>();
            }

            return new MvcHtmlString(PropertyForHtmlGenerator.GenerateHtml(html.ViewData.Model, expression, result.ToString()));
        }

        static IAddPageEditingHtmlGenerator AddPageEditingHtmlGenerator { get; set; }

        public static MvcHtmlString AddPageEditing(this HtmlHelper html)
        {
            if (ModeProvider == null)
            {
                ModeProvider = DependencyResolver.Current.GetService<IModeProvider>();
            }

            if (ModeProvider.GetIsPageEditing(html.ViewContext.HttpContext) != true)
            {
                return null;
            }

            if(AddPageEditingHtmlGenerator == null)
            {
                AddPageEditingHtmlGenerator = DependencyResolver.Current.GetService<IAddPageEditingHtmlGenerator>();
            }

            return MvcHtmlString.Create(AddPageEditingHtmlGenerator.GenerateHtml());
        }
    }
}
