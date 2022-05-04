using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DateTimeHelper
    {
        #region Current

        public static DateTime CurrentMDateTime => DateTime.Now;
        public static DateTime CurrentMDateTimeUtc => DateTime.UtcNow;
        public static string CurrentMDateTimeString => CurrentMDateTime.ToString();
        public static string CurrentMDateTimeUtcString => CurrentMDateTimeUtc.ToString();
        public static int CurrentMMont => CurrentMDateTime.Month;
        public static int CurrentMYear => CurrentMDateTime.Year;
        public static int CurrentMDay => CurrentMDateTime.Day;
        public static int CurrentMHour => CurrentMDateTime.Hour;
        public static int CurrentMMinute => CurrentMDateTime.Minute;
        public static string CurrentMMonthName => CurrentMDateTime.ToString("MMMM");
        public static string CurrentMDayOfWeek => CurrentMDateTime.ToString("dddd");
        public static int CurrentMSecond => CurrentMDateTime.Second;
        public static int CurrentMMillisecond => CurrentMDateTimeUtc.Millisecond;
        public static int CurrentMMontUtc => CurrentMDateTimeUtc.Month;
        public static int CurrentMYearUtc => CurrentMDateTimeUtc.Year;
        public static int CurrentMDayUtc => CurrentMDateTimeUtc.Day;
        public static int CurrentMHourUtc => CurrentMDateTimeUtc.Hour;
        public static int CurrentMMinuteUtc => CurrentMDateTimeUtc.Minute;
        public static int CurrentMSecondUtc => CurrentMDateTimeUtc.Second;
        public static int CurrentMMillisecondUtc => CurrentMDateTimeUtc.Millisecond;
        public static string CurrentMMonthNameUtc => CurrentMDateTime.ToString("MMMM");
        public static string CurrentMDayOfWeekUtc => CurrentMDateTime.ToString("dddd");
        public static string GetCurrentShDate => CurrentMDateTime.ToString("yyyy/MM/dd",
            CultureManager.ShCultureInfo);
        public static string GetCurrentShMonthName => CurrentMDateTime.ToString("MMMM",
            CultureManager.ShCultureInfo);
        public static string GetCurrentShDayOfWeekName => CurrentMDateTime.ToString("dddd",
            CultureManager.ShCultureInfo);
        #endregion
        #region DatePart
        public static DateTimePart DateTimeMPart => GetDateTimePart(CurrentMDateTime);
        public static DateTimePart DateTimeMPartUtc => GetDateTimePart(CurrentMDateTimeUtc);
        public static DateTimePart GetDateTimePart(DateTime dateTime)
        {
            return new DateTimePart
            {
                Year = dateTime.Year,
                Day = dateTime.Day,
                Minute = dateTime.Minute,
                Millisecond = dateTime.Millisecond,
                Hour = dateTime.Hour,
                Month = dateTime.Month,
                MonthName = dateTime.ToString("MMMM",
                    CultureManager.ShCultureInfo),
                DayOfWeekName = dateTime.ToString("dddd",
                    CultureManager.ShCultureInfo),
                Second = dateTime.Second,
            };
        }
        public static DateTimePart GetDateTimeShPart(DateTime dateTime)
        {
            return new DateTimePart
            {
                Year = dateTime.Year,
                Day = dateTime.Day,
                Minute = dateTime.Minute,
                Millisecond = dateTime.Millisecond,
                Hour = dateTime.Hour,
                Month = dateTime.Month,
                MonthName = dateTime.ToString("MMMM", CultureManager.ShCultureInfo),
                DayOfWeekName = dateTime.ToString("dddd", CultureManager.ShCultureInfo),
                Second = dateTime.Second,
            };
        }
        #endregion
        #region DateDiff
        public static int ShDiffDay(DateTime firstDate, DateTime secondDate) => (firstDate - secondDate).Days;
        public static int ShDiffDay(string firstDate, string secondDate) => ShDiffDay(ToDateTime(firstDate), ToDateTime(secondDate));
        public static int ShDiffMonth(DateTime firstDate, DateTime secondDate) => ((firstDate.Year - secondDate.Year) * 12) + firstDate.Month - secondDate.Month;
        public static int ShDiffMonth(string firstDate, string secondDate) => ShDiffMonth(ToDateTime(firstDate), ToDateTime(secondDate));
        public static int ShDiffYear(DateTime firstDate, DateTime secondDate) => (firstDate.Year - secondDate.Year);
        public static int ShDiffYear(string firstDate, string secondDate) => ShDiffYear(ToDateTime(firstDate), ToDateTime(secondDate));
        #endregion
        #region Convert
        public static DateTime ToLocalDateTime(DateTime dateTimeUtc) => dateTimeUtc.ToLocalTime();
        public static string ToLocalDateTimeString(DateTime dateTimeUtc) =>
            dateTimeUtc.ToLocalTime().ToString("yyyy/MM/dd");
        public static string ToLocalDateTimeSh(DateTime dateTimeUtc) =>
           dateTimeUtc.ToString("yyyy/MM/dd");
        public static string ToDateTimeSh(DateTime dateTime) =>
           dateTime.ToString("yyyy/MM/dd",
            CultureManager.ShCultureInfo);

        public static DateTime ToDateTime(string dateTimeSh) => DateTime.Parse(dateTimeSh, CultureManager.ShCultureInfo);
        #endregion
    }
    public class DateTimePart
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public int Millisecond { get; set; }
        public string MonthName { get; set; }
        public string DayOfWeekName { get; set; }
    }
}
