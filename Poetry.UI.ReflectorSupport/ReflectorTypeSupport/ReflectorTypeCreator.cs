using System;
using System.Collections.Generic;
using System.Text;
using Poetry.UI.ReflectorSupport.ReflectorAttributeSupport;

namespace Poetry.UI.ReflectorSupport.ReflectorTypeSupport
{
    public class ReflectorTypeCreator : IReflectorTypeCreator
    {
        IReflectorAttributeCreator ReflectorAttributeCreator { get; }

        public ReflectorTypeCreator(IReflectorAttributeCreator reflectorAttributeCreator)
        {
            ReflectorAttributeCreator = reflectorAttributeCreator;
        }

        public ReflectorType CreateReflectorType(Type type)
        {
            if(type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return new ReflectorType(type.Name, ReflectorAttributeCreator.CreateReflectorAttributes(type));
        }
    }
}
