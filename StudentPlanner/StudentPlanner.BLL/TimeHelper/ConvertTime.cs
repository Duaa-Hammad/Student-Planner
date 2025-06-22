using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.TimeHelper
{
    public static class ConvertTime
    {
        private static readonly TimeZoneInfo LibyaTimeZone =
        TimeZoneInfo.CreateCustomTimeZone("Libya", TimeSpan.FromHours(2), "Libya Time", "Libya Time");

        public static DateTime ToLibyaTime(DateTime utcDate)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcDate, LibyaTimeZone);
        }

        public static DateTime ToUtcFromLibya(DateTime libyaDate)
        {
            var unspecified = DateTime.SpecifyKind(libyaDate, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeToUtc(unspecified, LibyaTimeZone);
        }
    }
}
