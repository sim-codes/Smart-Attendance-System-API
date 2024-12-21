using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IFacultyService
    {
        IEnumerable<FacultyDto> GetAllFaculties(bool trackChanges);
    }
}
