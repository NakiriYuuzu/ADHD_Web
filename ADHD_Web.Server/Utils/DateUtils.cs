namespace ADHD_Web.Server.Utils;

public static class DateUtils
{
    public static DateTime ToTaipeiTime()
    {
        TimeZoneInfo taipeiZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Taipei"); 
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, taipeiZone);
    }
}