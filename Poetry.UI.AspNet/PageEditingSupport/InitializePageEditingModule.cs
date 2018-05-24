using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Poetry.UI.AspNet.PageEditingSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(InitializePageEditingModule), "RegisterOnPageEditing")]

namespace Poetry.UI.AspNet.PageEditingSupport
{
    public class InitializePageEditingModule
    {
        public static void RegisterOnPageEditing()
        {
            DynamicModuleUtility.RegisterModule(typeof(PageEditingModule));
        }
    }
}