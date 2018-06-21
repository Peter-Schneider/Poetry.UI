using System.Collections.Generic;

namespace Poetry.UI.FormSupport
{
    public interface IFormProvider
    {
        IEnumerable<Form> GetAll();
    }
}