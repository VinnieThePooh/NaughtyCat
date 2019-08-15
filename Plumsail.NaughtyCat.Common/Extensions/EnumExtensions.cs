using System;
using System.ComponentModel;

namespace Plumsail.NaughtyCat.Common.Extensions
{
    public static class EnumExtensions
    {
        // enum value is fallback
        public static string GetEnumDescription<TEnum>(this TEnum enumValue)
            where TEnum : struct
        {
            var t = typeof(TEnum);
            if (!t.IsEnum)
            {
                throw new ArgumentException("Data type is not an enumeration", nameof(enumValue));
            }

            var fi = t.GetField(enumValue.ToString());

            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes &&
                attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return enumValue.ToString();
        }
    }
}