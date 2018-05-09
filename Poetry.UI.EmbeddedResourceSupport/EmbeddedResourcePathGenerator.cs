using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResourcePathGenerator : IEmbeddedResourcePathGenerator
    {
        public string GeneratePath(string resourceName)
        {
            var remaining = new Queue<string>(resourceName.Split('.'));
            var result = new StringBuilder();

            while (remaining.Any()) {
                var current = remaining.Dequeue();

                if (result.Length > 0)
                {
                    if (char.IsUpper(current[0]) || remaining.Count > 1)
                    {
                        result.Append('/');
                    }
                    else
                    {
                        result.Append('.');
                    }
                }

                result.Append(current);
            }

            return result.ToString();
        }
    }
}
