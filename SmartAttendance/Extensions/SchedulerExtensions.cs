using Quartz;

namespace SmartAttendance.Extensions
{
    public static class SchedulerExtensions
    {
        public static async Task<bool> IsJobRunning(this IScheduler scheduler, JobKey jobKey)
        {
            var currentlyExecutingJobs = await scheduler.GetCurrentlyExecutingJobs();
            return currentlyExecutingJobs.Any(job => job.JobDetail.Key.Equals(jobKey));
        }
    }
}
