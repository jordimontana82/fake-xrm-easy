using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
