using Entities.Models;

namespace Contracts
{
    public interface ILecturerRepository
    {
        Task<IEnumerable<Lecturer>> GetAllLecturersAsync(bool trackChanges);
        Task<Lecturer> GetLecturerAsync(string userId, bool trackChanges);
        void CreateLecturer(string userId, Lecturer lecturer);
    }
}
