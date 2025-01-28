using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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

        public void DeleteClassSchedule(ClassSchedule schedule) => Delete(schedule);

        public async Task<ClassSchedule> GetActiveScheduleForCourseAsync(Guid courseId, bool trackChanges)
        {
            var currentTime = DateTime.Now;
            var currentDayOfWeek = currentTime.DayOfWeek.ToString();
            var currentTimeOnly = TimeOnly.FromDateTime(currentTime);

            var activeSchedule = await FindByCondition(cs =>
                cs.CourseId.Equals(courseId) &&
                cs.DayOfWeek == currentDayOfWeek &&
                cs.StartTime <= currentTimeOnly &&
                cs.EndTime >= currentTimeOnly,
                trackChanges)
                .Include(cs => cs.Classroom)
                .SingleOrDefaultAsync();

            return activeSchedule;
        }

        public IEnumerable<ClassSchedule> GetClassSchedulesByCourseIds(IEnumerable<Guid> courseIds, bool trackChanges) =>
            FindByCondition(cs => courseIds.Contains(cs.CourseId), trackChanges)
            .Include(s => s.Course)
            .Include(s => s.Classroom)
            .ToList();
    }
}
