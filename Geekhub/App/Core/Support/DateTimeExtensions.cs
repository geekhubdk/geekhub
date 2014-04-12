using System;

namespace Geekhub.App.Core.Support
{
    public static class DateTimeExtensions
    {
        public static string ToNiceDate(this DateTime dateTime)
        {
            string prefix = "";
            var now = DateTime.Now;
            var daysUntil = (int)((dateTime.Date - now.Date).TotalDays);

            if (daysUntil == 0)
                prefix = "I dag, den";
            if (daysUntil == 1)
                prefix = "I morgen, den";
            if (daysUntil > 1 && daysUntil <= 6)
                prefix = string.Format("På {0}, den", dateTime.ToString("dddd"));

            return (prefix + " " + dateTime.ToString("dd. MMM yyyy, HH:mm")).Trim();
        }
    }
}