using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekBangCN.InfoPathAnalyzer.Utility
{
    public class StringHelper
    {
        private StringHelper()
        {
        }

        /// <summary>
        /// Seach the specific string in a reserve(previous) direction.
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static Int32 ReverseIndexOf(String sourceStr, String keyStr, Int32 startIndex)
        {
            String prevString = sourceStr.Substring(0, startIndex + 1);
            return prevString.LastIndexOf(keyStr);
        }

        public static String ReverseString(String sourceStr)
        {
            char[] arr = sourceStr.ToCharArray();
            Array.Reverse(arr);
            return new String(arr);
        }

        /// <summary>
        /// Remove the namespace of a string like.
        /// For example, the "xsd:" will be removed from "xsd:element". The final result is "element".
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static String RemoveNamespace(String name)
        {
            Int32 seperateCharIndex = name.IndexOf(":");
            while (seperateCharIndex != -1)
            {
                name = name.Substring(seperateCharIndex + 1);
                seperateCharIndex = name.IndexOf(":");
            }

            return name;
        }

        public static Boolean BlurCompare(String srcStr, String cmpStr, Boolean isCaseSensitive)
        {
            StringComparison comparison = isCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            return cmpStr.StartsWith(srcStr, comparison);
        }
    }
}
