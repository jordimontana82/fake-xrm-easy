using System;
using System.Globalization;

namespace FakeXrmEasy.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDayOfWeek(this DateTime dateTime, Int32 week, DayOfWeek dayOfWeek)
        {
            DateTime startOfYear = dateTime.AddDays(1 - dateTime.DayOfYear);
            return startOfYear.AddDays(7 * (week - 2) + ((dayOfWeek - startOfYear.DayOfWeek + 7) % 7));
        }

        public static DateTime ToDayOfDeltaWeek(this DateTime dateTime, Int32 deltaWeek, DayOfWeek dayOfWeek)
            => dateTime.ToDayOfWeek(CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dateTime
                , CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule
                , CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek) + deltaWeek, dayOfWeek);

        public static DateTime ToLastDayOfDeltaWeek(this DateTime dateTime, Int32 deltaWeek = 0)
            => dateTime.ToDayOfDeltaWeek(deltaWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek).AddDays(6);

        public static DateTime ToFirstDayOfDeltaWeek(this DateTime dateTime, Int32 deltaWeek = 0)
            => dateTime.ToDayOfDeltaWeek(deltaWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

        public static DateTime ToFirstDayOfMonth(this DateTime dateTime, Int32 month)
            => dateTime.AddDays(1 - dateTime.Day).AddMonths(month - dateTime.Month);

        public static DateTime ToFirstDayOfMonth(this DateTime dateTime)
            => dateTime.ToFirstDayOfMonth(dateTime.Month);

        public static DateTime ToLastDayOfMonth(this DateTime dateTime, Int32 month)
        {
            Int32 addYears = month > 12 ? month % 12 : 0;
            month = month - 12 * addYears;
            return dateTime
                .AddDays(CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(dateTime.Year + addYears, month) - dateTime.Day)
                .AddMonths(month - dateTime.Month).AddYears(addYears);
        }

        public static DateTime ToLastDayOfMonth(this DateTime dateTime)
            => dateTime.ToLastDayOfMonth(dateTime.Month);
    }
}
