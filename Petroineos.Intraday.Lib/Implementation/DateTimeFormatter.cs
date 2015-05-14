using System;

namespace Petroineos.Intraday.Lib.Implementation
{
    public class DateTimeFormatter : IDateTimeFormatter
    {
        public string GetFormattedTime(int period)
        {
            return (period == 2
                ? DateTime.Today.Add(new TimeSpan(24, 0, 0)).ToString("HH:mm")
                : (period < 2)
                    ? DateTime.Today.Add(new TimeSpan(24 - period, 0, 0)).ToString("HH:mm")
                    : DateTime.Today.Add(new TimeSpan(24 + period - 2, 0, 0)).ToString("HH:mm"));
        }
    }
}