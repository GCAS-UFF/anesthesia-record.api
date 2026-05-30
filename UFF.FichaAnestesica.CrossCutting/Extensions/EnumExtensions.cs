using System.ComponentModel;
using System.Reflection;

namespace UFF.FichaAnestesica.CrossCutting.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(T enumValue) where T : Enum
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());

            if (field == null)
                return enumValue.ToString();

            var attribute = field.GetCustomAttribute<DescriptionAttribute>();

            return attribute?.Description ?? enumValue.ToString();
        }
    }
}
