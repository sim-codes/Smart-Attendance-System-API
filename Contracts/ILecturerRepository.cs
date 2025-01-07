using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface ILecturerRepository
    {
        Task<PagedList<Lecturer>> GetAllLecturersAsync(LecturerParameters lecturerParameters, bool trackChanges);
        Task<Lecturer> GetLecturerAsync(string userId, bool trackChanges);
        void CreateLecturer(string userId, Lecturer lecturer);
    }
}
