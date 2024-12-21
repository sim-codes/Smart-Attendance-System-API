using Entities.Models;

namespace Service.Contracts
{
    public interface IFacultyService
    {
        IEnumerable<Faculty> GetAllFaculties(bool trackChanges);
    }
}
