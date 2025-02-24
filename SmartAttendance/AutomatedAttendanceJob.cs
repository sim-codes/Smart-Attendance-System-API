using Contracts;
using Quartz;
using Service.Contracts;

namespace SmartAttendance
{
    [DisallowConcurrentExecution]
    public class AutomatedAttendanceJob : IJob
    {
        private readonly IServiceManager _service;
        private readonly ILoggerManager _logger;

        public AutomatedAttendanceJob(IServiceManager service, ILoggerManager logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await _service.AttendanceService.AutoSignAttendanceForActiveClasses();
                _logger.LogInfo($"Automated attendance signing completed at: {DateTimeOffset.Now}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while signing automated attendance: {ex}");
                throw;
            }
        }
    }
}
