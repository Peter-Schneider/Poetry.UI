using Poetry.UI.PageEditingSupport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Poetry.UI.AspNet.PageEditingSupport
{
    public class ModeProvider : IModeProvider
    {
        public bool GetIsPageEditing(object context)
        {
            IDictionary items = null;

            if(context is HttpContext)
            {
                items = ((HttpContext)context).Items;
            }

            if(context is HttpContextWrapper)
            {
                items = ((HttpContextWrapper)context).Items;
            }

            if(items == null)
            {
                throw new ArgumentException("Context was neither HttpContext nor HttpContextWrapper");
            }

            if (!items.Contains("PoetryPageEditing"))
            {
                return false;
            }

            return items["PoetryPageEditing"] as bool? ?? false;
        }

        public void SetIsPageEditing(object context, bool value)
        {
            IDictionary items = null;

            if (context is HttpContext)
            {
                items = ((HttpContext)context).Items;
            }

            if (context is HttpContextWrapper)
            {
                items = ((HttpContextWrapper)context).Items;
            }

            if (items == null)
            {
                throw new ArgumentException("Context was neither HttpContext nor HttpContextWrapper");
            }

            items["PoetryPageEditing"] = true;
        }
    }
}
