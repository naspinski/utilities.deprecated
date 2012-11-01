using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Naspinski.Utilities
{
    public class Url
    {
        public static Uri Combine(params string[] parts)
        {
            if (parts.Length < 1) throw new InvalidOperationException("UriCombine must have at least one string as input");
            Uri uriBase = new Uri(parts[0]);
            if (parts.Length == 1) return uriBase;
            string relative = Path.Combine(parts.AsEnumerable().Skip(1).Select(x => x.TrimStart(new[] {'\\','/'})).ToArray());
            return new Uri(uriBase, relative);
        }
    } 
}
