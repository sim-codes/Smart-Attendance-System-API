﻿using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassroomRepository : RepositoryBase<Classroom>, IClassroomRepository
    {
        public ClassroomRepository(RepositoryContext context)
            : base(context)
        {

        }

        public IEnumerable<Classroom> GetClassrooms(Guid facultyId, bool trackChanges) =>
            FindByCondition(e => e.FacultyId.Equals(facultyId), trackChanges)
            .OrderBy(e => e.Name).ToList();

        public Classroom GetClassroom(Guid facultyId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.FacultyId.Equals(facultyId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public void CreateClassroom(Guid facultyId, Classroom classroom)
        {
            classroom.FacultyId = facultyId;
            Create(classroom);
        }

        public async Task<Classroom> GetClassroomByCourseScheduleAsync(Guid scheduleId, bool trackChanges) =>
            await FindByCondition(c => c.ClassSchedules.Any(cs => cs.Id.Equals(scheduleId)), trackChanges)
            .SingleOrDefaultAsync();

        public void DeleteClassroom(Classroom classroom) => Delete(classroom);
    }
}
