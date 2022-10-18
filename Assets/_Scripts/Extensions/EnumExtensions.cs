using System;
using System.Linq;

namespace _Scripts.Extensions
{
    public static class EnumExtensions
    {
        public static TEnum GetRandomEnumValue<TEnum>() where TEnum : Enum
        {
            return (TEnum)GetEnumValues<TEnum>().OfType<Enum>().OrderBy(_ => Guid.NewGuid()).FirstOrDefault();
        }

        public static TEnum[] GetEnumValues<TEnum>() where TEnum : Enum
        {
            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }
    }
}