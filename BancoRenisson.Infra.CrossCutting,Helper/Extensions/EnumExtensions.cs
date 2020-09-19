using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Envolva.Infra.CrossCutting.Helper.Extensions
{
    public static class EnumExtensions
    {
        public class NameValue
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }

        public static List<NameValue> EnumToList<T>()
        {
            var array = (T[])(Enum.GetValues(typeof(T)).Cast<T>());
            var array2 = Enum.GetNames(typeof(T)).ToArray<string>();
            List<NameValue> lst = null;
            for (int i = 0; i < array.Length; i++)
            {
                if (lst == null)
                    lst = new List<NameValue>();

                var name = array2[i];
                T value = array[i];
                var type = value.GetType();
                var typeName = Enum.GetName(type, value);
                var field = type.GetField(typeName);
                var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (fds.Length > 0)
                {
                    lst.Add(new NameValue { Name = ((DescriptionAttribute)fds[0]).Description, Value = value });
                }
                else
                {
                    lst.Add(new NameValue { Name = name, Value = value });
                }
            }
            return lst;
        }

        public static T? ToEnum<T>(this string value) where T : struct
        {
            if (string.IsNullOrEmpty(value)) return default(T);
            return
                Enum.TryParse<T>(value, true, out T result)
                ? result
                : default;
        }

        public static T? ToEnum<T>(this int value) where T : struct
        {
            if (string.IsNullOrEmpty(value.ToString())) return default(T);
            return
                Enum.TryParse<T>(value.ToString(), true, out T result)
                ? result
                : default;
        }

        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            dynamic displayAttribute = null;

            if (attributes.Any())
            {
                displayAttribute = attributes.ElementAt(0);
            }

            return displayAttribute?.Description ?? "Description Not Found";
        }

        private static void CheckIsEnum<T>(bool withFlags)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException(string.Format("Type '{0}' is not an enum", typeof(T).FullName));
            if (withFlags && !Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
                throw new ArgumentException(string.Format("Type '{0}' doesn't have the 'Flags' attribute", typeof(T).FullName));
        }

        public static bool IsFlagSet<T>(this T value, T flag) where T : struct
        {
            CheckIsEnum<T>(true);
            long lValue = Convert.ToInt64(value);
            long lFlag = Convert.ToInt64(flag);
            return (lValue & lFlag) != 0;
        }

        public static IEnumerable<T> GetFlags<T>(this T value) where T : struct
        {
            CheckIsEnum<T>(true);
            foreach (T flag in Enum.GetValues(typeof(T)).Cast<T>())
            {
                if (value.IsFlagSet(flag))
                    yield return flag;
            }
        }

        public static T SetFlags<T>(this T value, T flags, bool on) where T : struct
        {
            CheckIsEnum<T>(true);
            long lValue = Convert.ToInt64(value);
            long lFlag = Convert.ToInt64(flags);
            if (on)
            {
                lValue |= lFlag;
            }
            else
            {
                lValue &= (~lFlag);
            }
            return (T)Enum.ToObject(typeof(T), lValue);
        }

        public static T SetFlags<T>(this T value, T flags) where T : struct
        {
            return value.SetFlags(flags, true);
        }

        public static T ClearFlags<T>(this T value, T flags) where T : struct
        {
            return value.SetFlags(flags, false);
        }

        public static T CombineFlags<T>(this IEnumerable<T> flags) where T : struct
        {
            CheckIsEnum<T>(true);
            long lValue = 0;
            foreach (T flag in flags)
            {
                long lFlag = Convert.ToInt64(flag);
                lValue |= lFlag;
            }
            return (T)Enum.ToObject(typeof(T), lValue);
        }
    }
}