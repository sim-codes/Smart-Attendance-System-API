using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace SmartAttendance
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EnrollmentForCreationDto, Enrollment>();
            CreateMap<Level, LevelDto>();
            CreateMap<LevelForCreationDto, Level>();
            CreateMap<Course, CourseDto>();
            CreateMap<CourseForCreationDto, Course>();
            CreateMap<AttendanceForCreationDto, Attendance>();
            CreateMap<Attendance, AttendanceDto>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<ClassSchedule, ClassScheduleDto>()
                .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.Classroom, opt => opt.MapFrom(src => src.Classroom.Name))
                .ForMember(dest => dest.LecturerId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<ClassScheduleForCreationDto, ClassSchedule>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.LecturerId));
            CreateMap<ClassScheduleForUpdateDto, ClassSchedule>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.LecturerId));

            CreateMap<AcademicSession, AcademicSessionDto>();
            CreateMap<Faculty, FacultyDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Classroom, ClassroomDto>();
            CreateMap<User, UserDto>();

            CreateMap<FacultyForCreationDto, Faculty>();
            CreateMap<AcademicSessionForCreationDto, AcademicSession>();
            CreateMap<DepartmentForCreationDto, Department>();
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<ClassroomForCreationDto, Classroom>();
            CreateMap<StudentForCreationDto, Student>();
            CreateMap<Student,  StudentDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.User.ProfileImageUrl))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level.Name))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<StudentForUpdateDto, Student>()
                .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForPath(dest => dest.User.ProfileImageUrl, opt => opt.MapFrom(src => src.ProfileImageUrl));

            CreateMap<LecturerForCreationDto, Lecturer>();
            CreateMap<Lecturer, LecturerDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.User.ProfileImageUrl))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<LecturerForUpdateDto, Lecturer>()
                .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForPath(dest => dest.User.ProfileImageUrl, opt => opt.MapFrom(src => src.ProfileImageUrl));

            CreateMap<Enrollment, EnrollmentDto>()
                .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.Course.Code))
                .ForMember(dest => dest.CreditUnits, opt => opt.MapFrom(src => src.Course.CreditUnits));

            CreateMap<Enrollment, EnrolledStudentDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
