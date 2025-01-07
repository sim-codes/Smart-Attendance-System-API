using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IStudentService
    {
        Task<(IEnumerable<StudentDto> students, MetaData metaData)> GetAllStudentsAsync(StudentParameters studentParameters, bool trackChanges);
        Task<StudentDto> GetStudentAsync(string userId, bool trackChanges);
        Task<StudentDto> CreateStudent(string userId, StudentForCreationDto student);
    }
}
