using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace SmartAttendance
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Faculty, FacultyDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Classroom, ClassroomDto>();
            CreateMap<FacultyForCreationDto, Faculty>();
            CreateMap<DepartmentForCreationDto, Department>();
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<ClassroomForCreationDto, Classroom>();
        }
    }
}
