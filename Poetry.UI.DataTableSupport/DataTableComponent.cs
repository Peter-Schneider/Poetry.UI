using Poetry.UI.ComponentSupport;
using Poetry.UI.StyleSupport;
using Poetry.UI.ScriptSupport;
using System;
using Poetry.UI.ComponentSupport.DependencySupport;

namespace Poetry.UI.TableSupport
{
    [Component("DataTable")]
    [Script("Scripts/data-table.js")]
    [Style("Styles/data-table.css")]
    [Dependency("Portal")]
    public class DataTableComponent
    {
    }
}
