using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassScheduleRepository : RepositoryBase<ClassSchedule>, IClassScheduleRepository
    {
        public ClassScheduleRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateClassSchedule(ClassSchedule schedule) => Create(schedule);

        public ClassSchedule GetClassSchedule(Guid scheduleId, bool trackChanges) =>
            FindByCondition(s => s.Id.Equals(scheduleId), trackChanges)
            .SingleOrDefault();

        public IEnumerable<ClassSchedule> GetClassSchedules(bool tackChanges) =>
            FindAll(tackChanges)
            .OrderBy(s => s.DayOfWeek)
            .ToList();
    }
}
