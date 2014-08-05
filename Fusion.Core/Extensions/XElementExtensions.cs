using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using Fusion.Core.Exceptions;

namespace Fusion.Core.Extensions
{
    public static class XElementExtensions
    {
        #region Values

        public static long ValueAsLong(this XElement element)
        {
            return element.ValueAs<long>();
        }

        public static decimal ValueAsDecimal(this XElement element)
        {
            return element.ValueAs<decimal>();
        }

        public static T ValueAsEnum<T>(this XElement element)
        {
            return (T)Enum.Parse(typeof(T), element.Value, true);
        }

        public static DateTime ValueAsDateTime(this XElement element)
        {
            return element.ValueAs<DateTime>();
        }

        public static T ValueAs<T>(this XElement element, bool required = true)
        {
            var value = element.Value;
            if (value == null)
                throw new ApiException("Value is blank");

            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter == null)
                throw new ApiException("Could not get TypeConverter for type {0}", typeof(T).ToString());

            if (!converter.IsValid(value))
                throw new ApiException("Could not convert value");

            return (T)(converter.ConvertFromInvariantString(value));
        }

        #endregion

        #region Attributes

        public static string AttributeAsString(this XElement element, string attributeName)
        {
            return element.AttributeAs<string>(attributeName);
        }

        public static int AttributeAsInt(this XElement element, string attributeName)
        {
            return element.AttributeAs<int>(attributeName);
        }

        public static int? AttributeAsNullableInt(this XElement element, string attributeName)
        {
            if ((element.Attribute(attributeName) == null) || (string.IsNullOrEmpty(element.Attribute(attributeName).Value)))
                return null;
            return element.AttributeAsInt(attributeName);
        }

        public static decimal AttributeAsDecimal(this XElement element, string attributeName)
        {
            return element.AttributeAs<decimal>(attributeName);
        }

        public static decimal? AttributeAsNullableDecimal(this XElement element, string attributeName)
        {
            if (element.Attribute(attributeName) == null || (string.IsNullOrEmpty(element.Attribute(attributeName).Value)))
                return null;
            return element.AttributeAsDecimal(attributeName);
        }

        public static long AttributeAsLong(this XElement element, string attributeName)
        {
            return element.AttributeAs<long>(attributeName);
        }

        public static long? AttributeAsNullableLong(this XElement element, string attributeName)
        {
            if (element.Attribute(attributeName) == null || (string.IsNullOrEmpty(element.Attribute(attributeName).Value)))
                return null;
            return element.AttributeAsLong(attributeName);
        }

        public static bool AttributeAsBool(this XElement element, string attributeName)
        {
            return element.AttributeAs<bool>(attributeName);
        }

        public static DateTime AttributeAsDateTime(this XElement element, string attributeName)
        {
            return element.AttributeAs<DateTime>(attributeName);
        }

        public static IList<T> AttributeAsList<T>(this XElement element, string attributeName, char separator = ',')
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter == null)
                throw new ApiException("Unable to get a converter");

            var list = new List<T>();
            var values = element.AttributeAsString(attributeName);
            foreach (var value in values.Split(separator))
            {
                if (converter.IsValid(value))
                {
                    list.Add((T)converter.ConvertFromInvariantString(value));
                }
            }
            return list;
        }

        public static T AttributeAsEnum<T>(this XElement element, string attributeName)
        {
            return (T)Enum.Parse(typeof(T), AttributeAsString(element, attributeName), true);
        }

        public static T AttributeAs<T>(this XElement element, string attributeName, bool required = true)
        {
            var attribute = element.Attribute(attributeName);
            var converter = TypeDescriptor.GetConverter(typeof(T));

            if (attribute == null)
                throw new ApiException("Attribute {0} is blank", attributeName);

            if (converter == null)
                throw new ApiException("Could not get TypeConverter for type {0}", typeof(T).ToString());

            var value = attribute.Value;
            if (!converter.IsValid(value))
                throw new ApiException("Could not convert value");

            return (T)(converter.ConvertFromInvariantString(value));
        }

        #endregion
    }
}