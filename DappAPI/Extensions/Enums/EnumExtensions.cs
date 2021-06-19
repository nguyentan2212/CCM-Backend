using System.ComponentModel;
using System.Reflection;

namespace DappAPI.Extensions.Enums
{
    public static class EnumExtensions
    {
        public static string ToDescriptionString<TEnum>(this TEnum @enum)
        {
            FieldInfo fieldInfo = @enum.GetType().GetField(@enum.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes?[0].Description ?? @enum.ToString();
        }
    }
}
