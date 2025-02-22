using Service.Contracts;

namespace SmartAttendance.Extensions
{
    public class HangfireJobs
    {
        //private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IServiceManager _serviceManager;

        //public HangfireJobs(IServiceScopeFactory serviceScopeFactory)
        //{
        //    _serviceScopeFactory = serviceScopeFactory;
        //}

        public HangfireJobs(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        //public void ProcessAttendance()
        //{
        //    using (var scope = _serviceScopeFactory.CreateScope())
        //    {
        //        var serviceManager = scope.ServiceProvider.GetRequiredService<IServiceManager>();
        //        serviceManager.AttendanceService.AutoSignAttendanceForActiveClasses();
        //    }
        //}

        public void ProcessAttendance()
        {
            _serviceManager.AttendanceService.AutoSignAttendanceForActiveClasses();
        }
    }
}
