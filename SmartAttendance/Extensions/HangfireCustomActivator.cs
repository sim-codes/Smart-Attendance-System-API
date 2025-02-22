using Hangfire;
using Service.Contracts;
using System.ComponentModel;

namespace SmartAttendance.Extensions
{
    public class HangfireCustomActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public HangfireCustomActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type jobType)
        {
            return _serviceProvider.GetService(jobType);
        }
    }
}
