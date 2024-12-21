using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class FacultyService : IFacultyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public FacultyService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<FacultyDto> GetAllFaculties(bool trackChanges)
        {
            try
            {
                var faculties = _repository.Faculty.GetAllFaculties(trackChanges);

                var facultiesDto = faculties.Select(faculty => new FacultyDto(
                    faculty.Id, faculty.Name ?? "", faculty.Code ?? "")).ToList();

                return facultiesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllFaculties)} service method {ex}");
                throw;
            }
        }
    }
}
