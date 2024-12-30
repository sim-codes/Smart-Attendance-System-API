using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared;
using Shared.DataTransferObjects;
using Entities.Exceptions;

namespace Service
{
    internal sealed class ClassScheduleService : IClassScheduleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ClassScheduleService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public ClassroomDto CreateClassSchedule(ClassScheduleForCreationDto classSchedule)
        {
            var scheduleEntity = _mapper.Map<ClassSchedule>(classSchedule);

            _repositoryManager.ClassSchedule.CreateClassSchedule(scheduleEntity);
            _repositoryManager.Save();

            var scheduleToReturn = _mapper.Map<ClassScheduleDto>(scheduleEntity);
            return scheduleEntity;
        }

        public ClassScheduleDto GetClassScheduleById(Guid Id, bool trackChanges)
        {
            var classSchedule = _repositoryManager.ClassSchedule.GetClassSchedule(Id, trackChanges);
            if (classSchedule is null)
                throw new ClassScheduleNotFoundException(Id);

            var classScheduleDto = _mapper.Map<ClassScheduleDto>(classSchedule);
            return classScheduleDto;
        }

        public IEnumerable<ClassScheduleDto> GetClassSchedules()
        {
            throw new NotImplementedException();
        }
    }
}
