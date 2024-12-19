using System;

namespace Gwynwhyvaar.Utils.Shared.Extensions
{
    public static class DateExtensions
    {
        /// <summary>
        /// Returns a new <see cref="System.DateTime"/> created by replacing the specified fields
        /// on the current <see cref="System.DateTime"/>
        /// </summary>
        /// <param name="year">The year (1 through 9999)</param>
        /// <param name="month">The month (1 through 12)</param>
        /// <param name="day">The day (1 through to the number of days in month)</param>
        /// <param name="hour">The hour (0 through 23)</param>
        /// <param name="minute">The minute (0 through 59)</param>
        /// <param name="second">The second (0 through 59)</param>
        /// <param name="millisecond">A value for milliseconds (0 through 999)</param>
        /// <returns>A new <see cref="System.DateTime"/></returns>
        public static DateTime Replace(this DateTime dt, int? year = null, int? month = null, int? day = null,
            int? hour = null, int? minute = null, int? second = null, int? millisecond = null)
        {
            year = year ?? dt.Year;
            month = month ?? dt.Month;
            day = day ?? dt.Day;
            hour = hour ?? dt.Hour;
            minute = minute ?? dt.Minute;
            second = second ?? dt.Second;
            millisecond = millisecond ?? dt.Millisecond;
            return new DateTime(year.Value, month.Value, day.Value,
                hour.Value, minute.Value, second.Value, millisecond.Value, dt.Kind);
        }

        /// <summary>
        /// Returns a <see cref="System.DateTime"/> representing the first day of the previous month.
        /// </summary>
        /// <returns>A new <see cref="System.DateTime"/></returns>
        public static DateTime FirstOfPreviousMonth(this DateTime dt)
        {
            return dt.AddMonths(-1).Replace(day: 1);
        }

        /// <summary>
        /// Returns a <see cref="System.DateTime"/> representing the first day of next month.
        /// </summary>
        /// <returns>A new <see cref="System.DateTime"/></returns>
        public static DateTime FirstOfNextMonth(this DateTime dt)
        {
            return dt.AddMonths(1).Replace(day: 1);
        }

        /// <summary>
        /// Returns a <see cref="System.DateTime"/> representing midnight on the current day.
        /// </summary>
        /// <returns>A new <see cref="System.DateTime"/></returns>
        public static DateTime Midnight(this DateTime dt)
        {
            return dt.Date;
        }

        #region Number-to-Date Convenience Methods

        /// <summary>
        /// Returns a new <see cref="System.DateTime"/> representing this number of seconds since <paramref name="time"/>.
        /// Called on a negative number, returns the number of seconds before <paramref name="time"/>.
        /// </summary>
        /// <param name="time">A <see cref="System.DateTime"/> representing our reference point.</param>
        /// <returns>A new <see cref="System.DateTime"/></returns>
        public static DateTime SecondsSince(this int secs, DateTime time)
        {
            return time.AddSeconds(secs);
        }

        /// <summary>
        /// Returns a new <see cref="System.DateTime"/> representing this number of seconds before now.
        /// </summary>
        /// <returns>A new <see cref="System.DateTime"/></returns>
        public static DateTime SecondsAgo(this int secs)
        {
            return (-1 * secs).SecondsSince(DateTime.Now);
        }

        /// <summary>
        /// Returns a new <see cref="System.DateTime"/> representing this number of seconds from now.
        /// </summary>
        /// <returns>A new <see cref="System.DateTime"/></returns>
        public static DateTime SecondsFromNow(this int secs)
        {
            return secs.SecondsSince(DateTime.Now);
        }

        /// <summary>
        /// Returns a new <see cref="System.DateTime"/> representing this number of hours since <paramref name="time"/>.
        /// Called on a negative number, returns the number of hours before <paramref name="time"/>.
        /// </summary>
        /// <param name="time">A <see cref="System.DateTime"/> representing our reference point.</param>
        /// <returns>A new <see cref="System.DateTime"/></returns>
        public static DateTime HoursSince(this int hours, DateTime time)
        {
            return time.AddHours(hours);
        }

        /// <summary>
        /// Returns a new <see cref="System.DateTime"/> representing this number of hours before now.
        /// </summary>
        /// <returns>A new <see cref="System.DateTime"/></returns>
        public static DateTime HoursAgo(this int hours)
        {
            return (-1 * hours).HoursSince(DateTime.Now);
        }

        /// <summary>
        /// Returns a new <see cref="System.DateTime"/> representing this number of hours from now.
        /// </summary>
        /// <returns>A new <see cref="System.DateTime"/></returns>
        public static DateTime HoursFromNow(this int hours)
        {
            return hours.HoursSince(DateTime.Now);
        }

        /// <summary>
        /// Returns a new <see cref="System.DateTime"/> representing this number of days since <paramref name="time"/>.
        /// Called on a negative number, returns the number of days before <paramref name="time"/>.
        /// </summary>
        /// <param name="time">A <see cref="System.DateTime"/> representing our reference point.</param>
        /// <returns>A new <see cref="System.DateTime"/></returns>
        public static DateTime DaysSince(this int days, DateTime time)
        {
            return time.AddDays(days);
        }

        /// <summary>
        /// Returns a new <see cref="System.DateTime"/> representing this number of days before now.
        /// </summary>
        /// <returns>A new <see cref="System.DateTime"/></returns>
        public static DateTime DaysAgo(this int days)
        {
            return (-1 * days).DaysSince(DateTime.Now);
        }

        /// <summary>
        /// Returns a new <see cref="System.DateTime"/> representing this number of days from now.
        /// </summary>
        /// <returns>A new <see cref="System.DateTime"/></returns>
        public static DateTime DaysFromNow(this int days)
        {
            return days.DaysSince(DateTime.Now);
        }

        /// <summary>
        /// Returns a <see cref="System.TimeSpan"/> representing this interval in seconds.
        /// </summary>
        /// <returns>A new <see cref="System.TimeSpan"/></returns>
        public static TimeSpan Seconds(this int secs)
        {
            return new TimeSpan(0, 0, 0, secs);
        }

        /// <summary>
        /// Returns a <see cref="System.TimeSpan"/> representing this interval in minutes.
        /// </summary>
        /// <returns>A new <see cref="System.TimeSpan"/></returns>
        public static TimeSpan Minutes(this int minutes)
        {
            return new TimeSpan(0, 0, minutes, 0);
        }

        /// <summary>
        /// Returns a <see cref="System.TimeSpan"/> representing this interval in hours.
        /// </summary>
        /// <returns>A new <see cref="System.TimeSpan"/></returns>
        public static TimeSpan Hours(this int hours)
        {
            return new TimeSpan(0, hours, 0, 0);
        }

        /// <summary>
        /// Returns a <see cref="System.TimeSpan"/> representing this interval in days.
        /// </summary>
        /// <returns>A new <see cref="System.TimeSpan"/></returns>
        public static TimeSpan Days(this int days)
        {
            return new TimeSpan(days, 0, 0, 0);
        }

        #endregion
    }
}