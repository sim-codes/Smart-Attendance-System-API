using Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AttendanceBackgroundService : BackgroundService
    {
        private readonly ILoggerManager _logger;
        private readonly IServiceProvider _serviceProvider;

        public AttendanceBackgroundService(ILoggerManager logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var attendanceService = scope.ServiceProvider.GetRequiredService<IAttendanceService>();
                        await attendanceService.AutoSignAttendanceForActiveClasses();
                        _logger.LogInfo($"Automated attendance signing completed at: {DateTimeOffset.Now}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occurred while signing automated attendance: {ex}");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
