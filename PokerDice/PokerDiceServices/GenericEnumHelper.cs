using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerDice.Services
{
    /// <summary>
    /// Generic Enum Helper.
    /// </summary>
    public static class GenericEnumHelper
    {
        /// <summary>
        /// Converts the enum with description.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="System.Exception">Type given T must be an Enum</exception>
        public static IEnumerable<T> ConvertEnumWithDescription<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("Type given must be an Enum");
            }

            var enumType = typeof(T).ToString().Split('.').Last();
            var itemsList = Enum.GetValues(typeof(T))
                .Cast<T>().ToList();
            return itemsList;
        }

        /// <summary>
        /// Gets the enum description.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetEnumDescription<T>(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }
    }
}
