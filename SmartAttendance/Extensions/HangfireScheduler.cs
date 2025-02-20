using Hangfire;
using Service.Contracts;

namespace SmartAttendance.Extensions
{
    public static class HangfireScheduler
    {
        public static void ScheduleRecurringJobs(IRecurringJobManager recurringJobManager)
        {
            recurringJobManager.AddOrUpdate(
                "auto-sign-attendance",
                () => Console.WriteLine("Recurring!"),
                "*/5 7-17 * * 1-5",
                TimeZoneInfo.Utc);
        }
    }
}
