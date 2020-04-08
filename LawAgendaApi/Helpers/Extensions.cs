using System;
using System.Globalization;
using System.Text;

namespace LawAgendaApi.Helpers
{
    public static class Extensions
    {
        public static string Capitalize(this string str)
        {
            if (str.Length == 0)
            {
                return string.Empty;
            }
            else if (str.Length == 1)
            {
                return char.ToUpper(str[0]).ToString();
            }
            else
            {
                return char.ToUpper(str[0]) + str.Substring(1);
            }
        }

        public static string ConvertToPascalCase(this string str)
        {
            var builder = new StringBuilder();

            foreach (var c in str)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    builder.Append(" ");
                }
                else
                {
                    builder.Append(c);
                }
            }

            var result = builder.ToString();
            result = result.ToLower();

            var textInfo = new CultureInfo("en-US", false).TextInfo;

            result = textInfo.ToTitleCase(result).Replace(" ", newValue: string.Empty);

            return result;
        }
        
        public static string ConvertToCamelCase(this string str)
        {
            str = str.ConvertToPascalCase();

            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            var a = str.ToCharArray();
            a[0] = char.ToLower(a[0]);
            
            return new string(a);
        }
        
        public static bool IsValueDouble(this string str)
        {
            return double.TryParse(str, out var num);
        }
    }
}