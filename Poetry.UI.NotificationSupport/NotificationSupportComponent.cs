using Poetry.UI.ComponentSupport;
using Poetry.UI.ComponentSupport.DependencySupport;
using Poetry.UI.ScriptSupport;
using Poetry.UI.StyleSupport;
using System;

namespace Poetry.UI.NotificationSupport
{
    [Component("Poetry.UI.NotificationSupport")]
    [Dependency("Portal")]
    [Style("Styles/notification-manager.css")]
    [Script("Scripts/notification-manager.js")]
    [Script("Scripts/notification.js")]
    public class NotificationSupportComponent
    {
    }
}
