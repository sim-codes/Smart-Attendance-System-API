using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AttendanceRepository : RepositoryBase<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateAttendance(Attendance attendance) => Create(attendance);

        public void CreateAttendanceRange(IEnumerable<Attendance> attendances) =>
            CreateRange(attendances);

        public PagedList<Attendance> GetAllAttendanceRecords(AttendanceParameters attendanceParameters,bool trackChanges)
        {
            var attendances = FindAll(trackChanges)
                .Search(attendanceParameters.SearchTerm)
                .FilterByStudent(attendanceParameters.UserId)
                .FilterByCourse(attendanceParameters.CourseId)
                .ToList();

            return PagedList<Attendance>
                .ToPagedList(attendances, attendanceParameters.PageNumber, attendanceParameters.PageSize);
        }

        public Attendance GetAttendanceById(Guid attendanceId, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(attendanceId), trackChanges)
            .SingleOrDefault();

        public async Task<IEnumerable<string>> GetAllSignedStudentIdsAsync(Guid courseId, DateTime today, bool trackChanges)
        {
            return await FindByCondition(a => a.CourseId.Equals(courseId) && a.RecordedAt.Date == today, trackChanges)
                .Select(a => a.UserId)
                .ToListAsync();
        }
    }
}
