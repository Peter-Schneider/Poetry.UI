using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.PageEditingSupport
{
    public interface IModeProvider
    {
        bool GetIsPageEditing(object context);
        void SetIsPageEditing(object context, bool value);
    }
}
