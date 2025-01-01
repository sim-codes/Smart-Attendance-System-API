using Contracts;
using Entities.Models;
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

        public IEnumerable<Attendance> GetAllAttendances(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.RecordedAt)
            .ToList();

        public Attendance GetAttendanceById(Guid attendanceId, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(attendanceId), trackChanges)
            .SingleOrDefault();
    }
}
