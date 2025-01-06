﻿using Contracts;
using Entities.Models;
using Shared.RequestFeatures;

namespace Repository
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<Course> GetDepartmentalCourses(Guid departmentId, CourseParameters courseParameters, bool trackChanges) =>
            FindByCondition(e => e.DepartmentId.Equals(departmentId), trackChanges)
            .OrderBy(e => e.Title)
            .Skip((courseParameters.PageNumber - 1) * courseParameters.PageSize)
            .Take(courseParameters.PageSize)
            .ToList();

        public Course GetDepartmentalCourse(Guid departmentId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.DepartmentId.Equals(departmentId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();
        public void CreateDepartmentalCourse(Guid departmentId, Course course)
        {
            course.DepartmentId = departmentId;
            Create(course);
        }

        public void DeleteDepartmentalCourse(Course course) => Delete(course);
    }
}
