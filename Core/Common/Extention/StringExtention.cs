using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extention
{
    public static class StringExtention
    {
        public static bool ContainsAny(this string text, params string[] values)
        {
            foreach (string value in values)
            {
                if (text.Contains(value))
                    return true;
            }

            return false;
        }
    }
}
