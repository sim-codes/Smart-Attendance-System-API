using AutoMapper;
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
        private readonly IMapper _mapper;

        public FacultyService(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<FacultyDto> GetAllFaculties(bool trackChanges)
        {
            try
            {
                var faculties = _repository.Faculty.GetAllFaculties(trackChanges);

                var facultiesDto = _mapper.Map<IEnumerable<FacultyDto>>(faculties);

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
