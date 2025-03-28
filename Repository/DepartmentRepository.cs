﻿using Entities.Models;
using Contracts;

namespace Repository
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Department> GetAllDepartments(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(e => e.Name).ToList();

        public IEnumerable<Department> GetFacultyDepartments(Guid facultyId, bool trackChanges) =>
            FindByCondition(e => e.FacultyId.Equals(facultyId), trackChanges)
            .OrderBy(e => e.Name).ToList();

        public Department GetFacultyDepartment(Guid facultyId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.FacultyId.Equals(facultyId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public void CreateDepartment(Guid facultyId, Department department)
        {
            department.FacultyId = facultyId;
            Create(department);
        }

        public Department GetDepartment(Guid id, bool trackChanges) =>
            FindByCondition(e => e.Id.Equals(id), trackChanges)
            .SingleOrDefault();
    }
} 
