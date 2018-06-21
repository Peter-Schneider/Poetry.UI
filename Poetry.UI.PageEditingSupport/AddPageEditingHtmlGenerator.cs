using Poetry.UI.RoutingSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.PageEditingSupport
{
    public class AddPageEditingHtmlGenerator : IAddPageEditingHtmlGenerator
    {
        IBasePathProvider BasePathProvider { get; }

        public AddPageEditingHtmlGenerator(IBasePathProvider basePathProvider)
        {
            BasePathProvider = basePathProvider;
        }

        public string GenerateHtml()
        {
            return $"<script src=\"/{BasePathProvider.BasePath}/PageEditing/Scripts/target-page-editor.js\" async></script>";
        }
    }
}
