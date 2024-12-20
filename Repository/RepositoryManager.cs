﻿using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private Lazy<IFacultyRepository> _facultyRepository;
        private Lazy<IDepartmentRepository> _departmentRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _facultyRepository = new Lazy<IFacultyRepository>(() => new FacultyRepository(_repositoryContext));
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_repositoryContext));
        }

        public IFacultyRepository Faculty => _facultyRepository.Value;

        public IDepartmentRepository Department => _departmentRepository.Value;

        public void Save() => _repositoryContext.SaveChanges();
    }
}
