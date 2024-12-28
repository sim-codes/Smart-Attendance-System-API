using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ILecturerService
    {
        Task<IEnumerable<LecturerDto>> GetLecturersAsync(bool trackChanges);
        Task<LecturerDto> GetLecturerAsync(string userId, bool trackChanges);
        Task<LecturerDto> CreateLecturer(string userId, LecturerForCreationDto lecturer);
    }
}
