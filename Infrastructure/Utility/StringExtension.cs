using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class StringExtension
    {
        public static string ToPascalCase(this string @string)
        {
            if (string.IsNullOrEmpty(@string))
                return string.Empty;
            if (@string.Length == 1)
                return @string.ToUpper();
            var firstChar = @string.Substring(0, 1).ToUpper();
            return firstChar + @string.Substring(1, @string.Length - 1);
        }
    }
}
