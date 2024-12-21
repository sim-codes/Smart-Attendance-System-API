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
        }
    }
}
