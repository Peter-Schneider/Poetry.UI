using Poetry.UI.PageEditingSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Poetry.UI.AspNet.PageEditingSupport
{
    public class PageEditingModule : IHttpModule
    {
        IModeProvider ModeProvider { get; set; }
        PathPrefixProvider PrefixProvider { get; set; }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += Context_BeginRequest;
        }

        void Context_BeginRequest(object sender, EventArgs e)
        {
            if (PrefixProvider == null)
            {
                PrefixProvider = DependencyResolver.Current.GetService<PathPrefixProvider>();
            }
            if (ModeProvider == null)
            {
                ModeProvider = DependencyResolver.Current.GetService<IModeProvider>();
            }

            var context = ((HttpApplication)sender).Context;

            if(!context.Request.Path.StartsWith(PrefixProvider.Prefix, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            ModeProvider.SetIsPageEditing(context, true);
            context.RewritePath("/" + context.Request.Path.Substring(PrefixProvider.Prefix.Length));
        }

        public void Dispose()
        {
        }
    }
}
