using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IFacultyService
    {
        IEnumerable<FacultyDto> GetAllFaculties(bool trackChanges);
        FacultyDto GetFaculty(Guid facultyId, bool trackChanges);
        FacultyDto CreateFaculty(FacultyForCreationDto faculty);
    }
}
