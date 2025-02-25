using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using System.Linq;

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

        public async Task AutoSignAttendanceForActiveClasses()
        {
            _logger.LogInfo("Starting automated attendance signing");

            var now = DateTime.Now;
            var currentTime = TimeOnly.FromDateTime(now);
            
            var endTime = currentTime.AddMinutes(10);

            var activeClasses = await _repository.ClassSchedule
                .GetClassSchedulesByTimeAsync(currentTime, endTime);

            foreach (var classSchedule in activeClasses)
            {
                try
                {
                    _logger.LogInfo($"Processing attendance for class {classSchedule.CourseId} scheduled to end at {classSchedule.EndTime}");

                    await MarkAbsentees(classSchedule.CourseId);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing attendance for class {classSchedule.CourseId}: {ex}");
                }
            }
        }

        private async Task MarkAbsentees(Guid courseId)
        {
            var now = DateTime.UtcNow;

            var studentIds = await _repository.CourseEnrollment.GetAllStudentEnrolledIdsForCourse(courseId, false);
            var signedStudentIds = await _repository.Attendance.GetAllSignedStudentIdsAsync(courseId, now.Date, false);

            var absentStudentIds = studentIds.Except(signedStudentIds).ToList();

            if (absentStudentIds.Any())
            {
                var attendanceRecords = absentStudentIds.Select(studentId => new Attendance
                {
                    Id = Guid.NewGuid(),
                    UserId = studentId,
                    CourseId = courseId,
                    RecordedAt = now,
                    Status = AttendanceStatus.Absent
                }).ToList();

                _repository.Attendance.CreateAttendanceRange(attendanceRecords);
                await _repository.SaveAsync();
            }
        }

        public async Task<AttendanceDto> CreateAttendance(string userId, AttendanceForCreationDto attendance)
        {
            var (isValid, message) = await ValiidateAttendance(attendance);

            if (!isValid)
                throw new AttendanceValidationException(message);

            var attendanceEntity = _mapper.Map<Attendance>(attendance);
            attendanceEntity.UserId = userId;
            attendanceEntity.RecordedAt = DateTime.UtcNow;

            _repository.Attendance.CreateAttendance(attendanceEntity);
            await _repository.SaveAsync();

            var attendanceDto = _mapper.Map<AttendanceDto>(attendanceEntity);
            return attendanceDto;
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

        public async Task<AttendanceDto> SignAttendanceWithoutLocation(string userId, AttendanceForCreationDto attendance)
        {
            var activeSchedule = await _repository.ClassSchedule.GetActiveScheduleForCourseAsync(attendance.CourseId, false);
            if (activeSchedule is null)
            {
                _logger.LogError($"No active schedule found for course with id {attendance.CourseId}");
                throw new AttendanceValidationException("No active schedule found for course");
            }

            var attendanceEntity = _mapper.Map<Attendance>(attendance);
            attendanceEntity.UserId = userId;
            attendanceEntity.RecordedAt = DateTime.UtcNow;

            _repository.Attendance.CreateAttendance(attendanceEntity);
            await _repository.SaveAsync();

            Console.WriteLine(attendanceEntity);

            var attendanceDto = _mapper.Map<AttendanceDto>(attendanceEntity);
            return attendanceDto;
        }

        public async Task<(bool isVaid, string message)> ValiidateAttendance(AttendanceForCreationDto attendance)
        {
            var activeSchedule = await _repository.ClassSchedule.GetActiveScheduleForCourseAsync(attendance.CourseId, false);
            if (activeSchedule is null)
            {
                _logger.LogError($"No active schedule found for course with id {attendance.CourseId}");
                return (false, "No active schedule found for course");
            }

            var classroom = await _repository.Classroom.GetClassroomByCourseScheduleAsync(activeSchedule.Id, false);

            var isWithinBoundary = IsPointWithinBoundary(
                attendance.StudentLat,
                attendance.StudentLon,
                classroom);

            if (!isWithinBoundary)
            {
                _logger.LogError($"Student is not within the boundary of the classroom with id {classroom.Id}");
                return (false, "Student is not within the boundary of the classroom");
            }
                return (true, "Location is verified successfully");
        }

        private bool IsPointWithinBoundary(double lat, double lon, Classroom classroom)
        {
            bool isWithinLatitude = lat >= Math.Min(classroom.BottomLeftLat, classroom.BottomRightLat) &&
                lat <= Math.Max(classroom.TopLeftLat, classroom.TopRightLat);

            bool isWithinLongitude = lon >= Math.Min(classroom.BottomLeftLon, classroom.TopLeftLon) &&
                lon <= Math.Max(classroom.BottomRightLon, classroom.TopRightLon);

            return isWithinLatitude && isWithinLongitude;
        }
    }
}
