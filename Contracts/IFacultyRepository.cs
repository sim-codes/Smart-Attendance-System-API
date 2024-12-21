using Entities.Models;

namespace Contracts
{
    public interface IFacultyRepository
    {
        IEnumerable<Faculty> GetAllFaculties(bool trackChanges);
    }
}
