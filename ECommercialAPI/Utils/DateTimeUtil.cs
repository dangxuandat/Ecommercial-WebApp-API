namespace ECommercialAPI.Utils;

public static class DateTimeUtil
{
    public static DateTime ConvertToVietNamDateTime(DateTime dateTime)
    {
        TimeZoneInfo targetTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        DateTime currentTimeInTargetTimeZone =
            TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, targetTimeZoneInfo);
        return currentTimeInTargetTimeZone;
    }
}