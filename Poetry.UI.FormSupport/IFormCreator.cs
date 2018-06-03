using System.Collections.Generic;

namespace Poetry.UI.FormSupport
{
    public interface IFormCreator
    {
        IEnumerable<Form> Create();
    }
}