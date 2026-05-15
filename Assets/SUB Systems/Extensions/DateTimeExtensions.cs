using System;

namespace SUBS.Core
{
    public static class DateTimeExtensions
    {
        public readonly static DateTime EpochStart = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static double GetCurrentTimeMilliSec()
        {
            return (DateTime.UtcNow - EpochStart).TotalMilliseconds;
        }

        public static double GetCurrentTimeMilliSec(this double dateTime)
        {
            return GetCurrentTimeMilliSec();
        }

        public static double GetCurrentTimeSec()
        {
            return (DateTime.UtcNow - EpochStart).TotalSeconds;
        }

        public static double GetCurrentTimeSec(this double dateTime)
        {
            return GetCurrentTimeSec();
        }

        public static double GetCurrentTimeMinut()
        {
            return (DateTime.UtcNow - EpochStart).TotalMinutes;
        }

        public static double GetCurrentTimeMinut(this double dateTime)
        {
            return GetCurrentTimeMinut();
        }
    }
}
