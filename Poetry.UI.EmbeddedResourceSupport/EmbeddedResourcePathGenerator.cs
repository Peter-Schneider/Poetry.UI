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
            var segments = resourceName.Split('.');

            if(HasUppercaseNonSegmentedFilename(segments))
            {
                return 
            }











            var builder = new StringBuilder();

            while(segments.Any())
            {
                var segment = segments.Dequeue();

                if (builder.Length > 0)
                {
                    if (char.IsUpper(segment[0]))
                    {
                        builder.Append('/');
                    }
                    else
                    {
                        builder.Append('.');
                    }
                }

                builder.Append(segment);
            }

            var result = builder.ToString();

            return result;
        }
    }
}
