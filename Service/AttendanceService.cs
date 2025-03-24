﻿using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using System.Linq;
using Shared.RequestFeatures;

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

        public (IEnumerable<AttendanceDto> attendanceRecords, MetaData metaData) GetAttendanceRecords(AttendanceParameters attendanceParameters, bool trackChanges)
        {
            var attendanceWithMetaData = _repository.Attendance.GetAllAttendanceRecords(attendanceParameters, trackChanges);
            var attendancesDto = _mapper.Map<IEnumerable<AttendanceDto>>(attendanceWithMetaData);

            return (attendanceRecords: attendancesDto, metaData: attendanceWithMetaData.MetaData);
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
                attendance.Accuracy,
                classroom);

            if (!isWithinBoundary)
            {
                _logger.LogError($"Student is not within the boundary of the classroom with id {classroom.Id}");
                return (false, "Student is not within the boundary of the classroom");
            }
                return (true, "Location is verified successfully");
        }

        private bool IsPointWithinBoundary(double lat, double lon, double accuracy, Classroom classroom)
        {
            var p1 = (classroom.TopLeftLat, classroom.TopLeftLon);
            var p2 = (classroom.TopRightLat, classroom.TopRightLon);
            var p3 = (classroom.BottomRightLat, classroom.BottomRightLon);
            var p4 = (classroom.BottomLeftLat, classroom.BottomLeftLon);
            var polygon = new (double, double)[] { p1, p2, p3, p4 };

            if (IsPointInsideQuadrilateral(lat, lon, polygon))
            {
                return true; // Strict check
            }

            // Convert accuracy to degrees
            double accuracyLat = accuracy / 111000;
            double accuracyLon = accuracy / (111000 * Math.Cos(lat * Math.PI / 180));

            // Try again with expanded boundary
            return IsPointInsideQuadrilateral(lat + accuracyLat, lon, polygon) ||
                   IsPointInsideQuadrilateral(lat - accuracyLat, lon, polygon) ||
                   IsPointInsideQuadrilateral(lat, lon + accuracyLon, polygon) ||
                   IsPointInsideQuadrilateral(lat, lon - accuracyLon, polygon);
        }

        private bool IsPointInsideQuadrilateral(double lat, double lon, (double lat, double lon)[] quad)
        {
            int intersections = 0;
            int count = quad.Length;

            for (int i = 0; i < count; i++)
            {
                var p1 = quad[i];
                var p2 = quad[(i + 1) % count]; // Wraps around to the first point

                if (((p1.lat > lat) != (p2.lat > lat)) &&
                    (lon < (p2.lon - p1.lon) * (lat - p1.lat) / (p2.lat - p1.lat) + p1.lon))
                {
                    intersections++;
                }
            }

            // Odd number of intersections means inside, even means outside
            return (intersections % 2) == 1;
        }

        // Helper function to check if a point is inside a triangle
        private bool IsPointInsideTriangle(double px, double py, (double x, double y) p1, (double x, double y) p2, (double x, double y) p3)
        {
            double areaOrig = TriangleArea(p1, p2, p3);
            double area1 = TriangleArea((px, py), p2, p3);
            double area2 = TriangleArea(p1, (px, py), p3);
            double area3 = TriangleArea(p1, p2, (px, py));

            // Check if sum of small triangle areas equals the original area
            return Math.Abs(areaOrig - (area1 + area2 + area3)) < 0.0000001;
        }

        // Compute the area of a triangle given three points
        private double TriangleArea((double x, double y) p1, (double x, double y) p2, (double x, double y) p3)
        {
            return Math.Abs((p1.x * (p2.y - p3.y) + p2.x * (p3.y - p1.y) + p3.x * (p1.y - p2.y)) / 2.0);
        }
    }
}
