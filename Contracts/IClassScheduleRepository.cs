using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IClassScheduleRepository
    {
        IEnumerable<ClassSchedule> GetClassSchedules(bool tackChanges);
        ClassSchedule GetClassSchedule(Guid scheduleId, bool trackChanges);
        void CreateClassSchedule(ClassSchedule schedule);
        Task<ClassSchedule> GetActiveScheduleForCourseAsync(Guid courseId, bool trackChanges);
    }
}
