using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Domain.Common
{
    public static class SystemClock
    {
        private static CultureInfo resourceCulture;

        private static DateTime? _customDate;

        public static DateTime Now
        {
            get
            {
                if (_customDate.HasValue)
                {
                    return _customDate.Value;
                }

                return DateTime.UtcNow;
            }
        }

        public static void Set(DateTime customDate) => _customDate = customDate;

        public static void Reset() => _customDate = null;

        public static DateTime? DateTimeParser(string dateTimeStr, string[] dateFmt)
        {
            // example: var dt = "2011-03-21 13:26".ToDate(new string[]{"yyyy-MM-dd HH:mm", 
            //                                                  "M/d/yyyy h:mm:ss tt"});
            // or simpler: 
            // var dt = "2011-03-21 13:26".ToDate("yyyy-MM-dd HH:mm", "M/d/yyyy h:mm:ss tt");

            const DateTimeStyles style = DateTimeStyles.AllowWhiteSpaces;
            if (dateFmt == null)
            {
                var dateInfo = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat;
                dateFmt = dateInfo.GetAllDateTimePatterns();
            }

            var result = DateTime.TryParseExact(dateTimeStr, dateFmt, CultureInfo.InvariantCulture, style, out var dt) ? dt : null as DateTime?;

            return result;
        }

        public static DateTime ChangeTime(DateTime dateTime, int hours, int minutes, int seconds = default, int milliseconds = default)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hours, minutes, seconds, milliseconds, dateTime.Kind);
        }

        public static DateTime? ConvertStringToDateTime(string value, string bankName = "MEDIRECTBANK")
        {
            try
            {
                DateTime parsedDate;

                var result = DateTime.TryParse(value, out parsedDate);

                return parsedDate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
