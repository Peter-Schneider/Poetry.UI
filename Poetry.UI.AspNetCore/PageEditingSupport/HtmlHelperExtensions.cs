using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Poetry.UI.PageEditingSupport;
using Poetry.UI.RoutingSupport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Poetry.UI.AspNetCore.PageEditingSupport
{
    public static class HtmlHelperExtensions
    {
        static IModeProvider ModeProvider { get; set; }
        static IPropertyExpressionMetaDataProvider PropertyExpressionMetaDataProvider { get; set; }
        static IObjectIdentifier ObjectIdentifier { get; set; }

        public static IHtmlContent PropertyFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            if (ModeProvider == null)
            {
                ModeProvider = (IModeProvider)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IModeProvider));
            }

            var result = html.DisplayFor(expression);

            if (ModeProvider.GetIsPageEditing(html.ViewContext.HttpContext) != true)
            {
                return result;
            }

            if (ObjectIdentifier == null)
            {
                ObjectIdentifier = (IObjectIdentifier)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IObjectIdentifier));
            }

            if (PropertyExpressionMetaDataProvider == null)
            {
                PropertyExpressionMetaDataProvider = (IPropertyExpressionMetaDataProvider)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IPropertyExpressionMetaDataProvider));
            }

            var metaData = PropertyExpressionMetaDataProvider.GetFor(expression);

            var listHtml = new HtmlContentBuilder();

            listHtml.AppendHtml($"<span class=\"poetry-page-editing-property\" property-name=\"{metaData.PropertyName}\" object-id=\"{ObjectIdentifier.GetId(metaData.OwnerObject ?? html.ViewData.Model)}\">");
            listHtml.AppendHtml(result);
            listHtml.AppendHtml("</span>");

            return listHtml;
        }

        static IBasePathProvider BasePathProvider { get; set; }

        public static IHtmlContent AddPageEditing(this IHtmlHelper html)
        {
            if (ModeProvider == null)
            {
                ModeProvider = (IModeProvider)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IModeProvider));
            }

            if (ModeProvider.GetIsPageEditing(html.ViewContext.HttpContext) != true)
            {
                return null;
            }

            if (BasePathProvider == null)
            {
                BasePathProvider = (IBasePathProvider)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IBasePathProvider));
            }

            return new HtmlString($"<script src=\"/{BasePathProvider.BasePath}/PageEditing/Scripts/target-page-editor.js\" async></script>");
        }
    }
}
