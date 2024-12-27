﻿using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;
using Entities.Exceptions;

namespace Service
{
    internal class StudentService : IStudentService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private UserManager<User> _userManager;

        public StudentService(IRepositoryManager repository, 
            ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager
            )
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<StudentDto> CreateStudent(StudentForCreationDto student)
        {
            var user = await _userManager.FindByIdAsync(student.UserId);
            if (user == null)
                throw new UserNotFoundException(student.UserId);

            var studentEntity = _mapper.Map<Student>(student);
            studentEntity.User = user;

            _repository.Student.CreateStudent(studentEntity);
            await _repository.SaveAsync();

            var createdStudent = _mapper.Map<StudentDto>(studentEntity);
            return createdStudent;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync(bool trackChanges)
        {
            var students = await _repository.Student.GetAllStudentsAsync(trackChanges);
            var studentsDto = _mapper.Map<IEnumerable<StudentDto>>(students);
            return studentsDto;
        }

        public async Task<StudentDto> GetStudentAsync(string userId, bool trackChanges)
        {
            var student = await _repository.Student.GetStudentAsync(userId, trackChanges);
            if (student is null)
                throw new StudentNotFoundException(userId);

            var studentDto = _mapper.Map<StudentDto>(student);
            return studentDto;
        }
    }
}