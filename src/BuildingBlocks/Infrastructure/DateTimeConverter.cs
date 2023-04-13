using System;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure;

public static class DateTimeConverter
{
    public static DateTime? MaybeUtcDateTime(DateTime? src)
    {
        if (src == null)
        {
            return null;
        }

        return UtcDateTime((DateTime)src);
    }

    public static DateTime UtcDateTime(DateTime src)
    {
        return src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc);
    }
}
