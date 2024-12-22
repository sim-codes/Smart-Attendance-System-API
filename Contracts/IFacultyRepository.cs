using Entities.Models;

namespace Contracts
{
    public interface IFacultyRepository
    {
        IEnumerable<Faculty> GetAllFaculties(bool trackChanges);
        Faculty GetFaculty(Guid facultyId, bool trackChanges);
        void CreateFaculty(Faculty faculty);
    }
}
