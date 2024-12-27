using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync(bool trackChanges);
        Task<StudentDto> GetStudentAsync(string userId, bool trackChanges);
        Task<StudentDto> CreateStudent(StudentForCreationDto student);
    }
}
