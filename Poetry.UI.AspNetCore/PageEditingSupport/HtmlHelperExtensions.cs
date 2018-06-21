using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Poetry.UI.PageEditingSupport;
using Poetry.UI.RoutingSupport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;

namespace Poetry.UI.AspNetCore.PageEditingSupport
{
    public static class HtmlHelperExtensions
    {
        static IModeProvider ModeProvider { get; set; }
        static IPropertyForHtmlGenerator PropertyForHtmlGenerator { get; set; }

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

            if (PropertyForHtmlGenerator == null)
            {
                PropertyForHtmlGenerator = (IPropertyForHtmlGenerator)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IPropertyForHtmlGenerator));
            }

            string resultString;

            using (var writer = new StringWriter())
            {
                result.WriteTo(writer, HtmlEncoder.Default);
                resultString = writer.ToString();
            }

            return new HtmlString(PropertyForHtmlGenerator.GenerateHtml(html.ViewData.Model, expression, resultString));
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
