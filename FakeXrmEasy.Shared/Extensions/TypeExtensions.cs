using Microsoft.Xrm.Sdk;
using System;

namespace FakeXrmEasy.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsOptionSet(this Type t)
        {
            var nullableType = Nullable.GetUnderlyingType(t);
            return t == typeof(OptionSetValue)
                || t.IsEnum
                || nullableType != null && nullableType.IsEnum;
        }

        public static bool IsDateTime(this Type t)
        {
            var nullableType = Nullable.GetUnderlyingType(t);
            return t == typeof(DateTime)
                || nullableType != null && nullableType == typeof(DateTime);
        }

        public static bool IsNullableEnum(this Type t)
        {
            return
                t.IsGenericType
                && t.GetGenericTypeDefinition() == typeof(Nullable<>)
                && t.GetGenericArguments()[0].IsEnum;
        }
    }
}