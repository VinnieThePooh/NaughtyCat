using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Plumsail.NaughtyCat.Common.Helpers
{
    public static class EnumsHelper
    {public static string GetEnumDescription<TEnum>(TEnum enumValue)
            where TEnum : struct
        {
            var t = typeof(TEnum);
            if (!t.IsEnum)
            {
                throw new ArgumentException("Data type is not an enumeration", nameof(enumValue));
            }

            var fi = t.GetField(enumValue.ToString());

            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return string.Empty;
        }
    }
}
