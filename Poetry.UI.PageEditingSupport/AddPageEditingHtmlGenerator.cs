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
            return
                $"<link rel=\"stylesheet\" type=\"text/css\" href=\"/{BasePathProvider.BasePath}/PageEditing/Styles/target-page-editor.css\" />" +
                $"<script src=\"/{BasePathProvider.BasePath}/PageEditing/Scripts/window-message-manager.js?target-page\"></script>" +
                $"<script src=\"/{BasePathProvider.BasePath}/PageEditing/Scripts/target-page-editor.js\"></script>";
        }
    }
}
