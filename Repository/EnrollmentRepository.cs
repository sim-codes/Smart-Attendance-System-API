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
    public class EnrollmentRepository : RepositoryBase<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(RepositoryContext context)
            : base(context)
        {
        }

        public void DeleteCourseEnrolledByStudent(Enrollment courseEnrollment) => Delete(courseEnrollment);

        public void EnrollStudentForCourse(string userId, Enrollment courseEnrollment)
        {
            courseEnrollment.UserId = userId;
            Create(courseEnrollment);
        }

        public Enrollment GetCourseEnrolledByStudent(string userId, Guid courseId, bool trackChanges) =>
            FindByCondition(e => e.UserId.Equals(userId) && e.CourseId.Equals(courseId), trackChanges)
            .Include(e => e.Course)
            .SingleOrDefault();

        public IEnumerable<Enrollment> GetStudentEnrolledCourses(string userId, bool trackChanges) =>
            FindByCondition(e => e.UserId.Equals(userId), trackChanges)
            .Include(e => e.Course)
            .OrderBy(e => e.EnrollmentDate).ToList();

        public async Task<IEnumerable<Enrollment>> GetStudentsEnrolledForCourse(Guid courseId, bool trackChanges) =>
            await FindByCondition(e => e.CourseId.Equals(courseId), trackChanges)
            .Include(e => e.User)
            .OrderBy(e => e.EnrollmentDate).ToListAsync();

        public async Task<IEnumerable<string>> GetAllStudentEnrolledIdsForCourse(Guid courseId, bool trackChanges)
        {
            var studentIds = await FindByCondition(e => e.CourseId.Equals(courseId), trackChanges)
                .Select(e => e.UserId).ToListAsync();
            return studentIds;
        }
    }
}
