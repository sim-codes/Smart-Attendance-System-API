using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;
using Entities.Exceptions;

namespace Service
{
    internal sealed class AttendanceService : IAttendanceService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public AttendanceService(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public AttendanceDto CreateAttendance(AttendanceForCreationDto attendance)
        {
            throw new NotImplementedException();
        }

        public AttendanceDto GetAttendance(Guid attendanceId, bool trackChanges)
        {
            var attendance = _repository.Attendance.GetAttendanceById(attendanceId, trackChanges);
            if (attendance is null)
                throw new AttendanceNotFoundException(attendanceId);

            var attendanceDto = _mapper.Map<AttendanceDto>(attendance);
            return attendanceDto;
        }

        public IEnumerable<AttendanceDto> GetAttendances(bool trackChanges)
        {
            var attendances = _repository.Attendance.GetAllAttendances(trackChanges);
            var attendancesDto = _mapper.Map<IEnumerable<AttendanceDto>>(attendances);
            return attendancesDto;
        }
    }
}
