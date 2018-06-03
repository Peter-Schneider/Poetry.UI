using Poetry.UI.ComponentSupport;
using Poetry.UI.ComponentSupport.DependencySupport;
using Poetry.UI.ScriptSupport;
using Poetry.UI.StyleSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.FormSupport
{
    [Component("Form")]
    [Script("Scripts/form-builder.js")]
    [Script("Scripts/form-field-provider.js")]
    [Script("Scripts/form-field-types.js")]
    [Style("Styles/form-elements.css")]
    [Dependency("Portal")]
    public class FormComponent
    {
    }
}
