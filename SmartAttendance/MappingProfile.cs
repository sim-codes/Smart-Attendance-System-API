using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace SmartAttendance
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClassSchedule, ClassScheduleDto>();
            CreateMap<ClassScheduleForCreationDto, ClassSchedule>();
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
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.User.ProfileImageUrl))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level.Name))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<LecturerForCreationDto, Lecturer>();
            CreateMap<Lecturer, LecturerDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.User.ProfileImageUrl))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name));
        }
    }
}
