using Poetry.UI.ReflectorSupport.ReflectorTypeSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ReflectorSupport
{
    public class Reflector : IReflector
    {
        IReflectorTypeCreator ReflectorTypeCreator { get; }
        Dictionary<Type, ReflectorType> Cache { get; } = new Dictionary<Type, ReflectorType>();

        public Reflector(IReflectorTypeCreator reflectorTypeCreator)
        {
            ReflectorTypeCreator = reflectorTypeCreator;
        }

        public ReflectorType GetReflectorType(Type type)
        {
            if(type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!Cache.ContainsKey(type))
            {
                Cache[type] = ReflectorTypeCreator.CreateReflectorType(type);
            }

            return Cache[type];
        }
    }
}
