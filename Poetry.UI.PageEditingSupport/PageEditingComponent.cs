using Poetry.UI.ComponentSupport;
using Poetry.UI.ComponentSupport.DependencySupport;
using Poetry.UI.ResourceSupport;
using Poetry.UI.ScriptSupport;
using Poetry.UI.StyleSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.PageEditingSupport
{
    [Component("PageEditing")]
    [Script("Scripts/page-editor.js")]
    [Script("Scripts/window-message-manager.js")]
    [Style("Styles/page-editor.css")]
    [Resource("Scripts/target-page-editor.js")]
    [Resource("Styles/target-page-editor.css")]
    [Dependency("Portal")]
    public class PageEditingComponent
    {
    }
}
