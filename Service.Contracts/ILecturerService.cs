using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ILecturerService
    {
        Task<(IEnumerable<LecturerDto> lecturers, MetaData metaData)> GetLecturersAsync(LecturerParameters lecturerParameters, bool trackChanges);
        Task<LecturerDto> GetLecturerAsync(string userId, bool trackChanges);
        Task<LecturerDto> CreateLecturer(string userId, LecturerForCreationDto lecturer);
        Task UpdateLecturer(string userId, LecturerForUpdateDto lecturer);
    }
}
