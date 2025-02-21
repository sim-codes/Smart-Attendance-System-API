using Hangfire;
using Service.Contracts;

namespace SmartAttendance.Extensions
{
    public static class HangfireScheduler
    {
        public static void ScheduleRecurringJobs(IRecurringJobManager recurringJobManager)
        {
            recurringJobManager.AddOrUpdate<IAttendanceService>(
                "auto-sign-attendance",
                service => service.AutoSignAttendanceForActiveClasses(),
                "*/5 7-17 * * 1-5",
                new RecurringJobOptions
                {
                    TimeZone = TimeZoneInfo.Utc
                });
        }
    }
}
