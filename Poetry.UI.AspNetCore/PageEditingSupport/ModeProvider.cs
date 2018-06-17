using Microsoft.AspNetCore.Http;
using Poetry.UI.PageEditingSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.AspNetCore.PageEditingSupport
{
    public class ModeProvider : IModeProvider
    {
        public bool GetIsPageEditing(object context)
        {
            IDictionary<object, object> items = null;

            if (context is HttpContext)
            {
                items = ((HttpContext)context).Items;
            }

            if (items == null)
            {
                throw new ArgumentException("Context was neither HttpContext nor HttpContextWrapper");
            }

            if (!items.ContainsKey("PoetryPageEditing"))
            {
                return false;
            }

            return items["PoetryPageEditing"] as bool? ?? false;
        }

        public void SetIsPageEditing(object context, bool value)
        {
            IDictionary<object, object> items = null;

            if (context is HttpContext)
            {
                items = ((HttpContext)context).Items;
            }

            if (items == null)
            {
                throw new ArgumentException("Context was neither HttpContext nor HttpContextWrapper");
            }

            items["PoetryPageEditing"] = true;
        }
    }
}
