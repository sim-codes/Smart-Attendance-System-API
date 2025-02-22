using Hangfire;
using Service.Contracts;

namespace SmartAttendance.Extensions
{
    public static class HangfireScheduler
    {
        public static void ScheduleRecurringJobs(IRecurringJobManager recurringJobManager)
        {
            recurringJobManager.AddOrUpdate<HangfireJobs>(
                "auto-sign-attendance",
                job => job.ProcessAttendance(),
                "*/5 7-17 * * 1-5",
                new RecurringJobOptions
                {
                    TimeZone = TimeZoneInfo.Utc
                });
        }
    }
}
