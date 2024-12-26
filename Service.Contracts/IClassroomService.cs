using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IClassroomService
    {
        IEnumerable<ClassroomDto> GetClassrooms(Guid facultyId, bool trackChanges);

        ClassroomDto GetClassroom(Guid facultyId, Guid id, bool trackChanges);
        ClassroomDto CreateClassroom(Guid facultyId, ClassroomForCreationDto classroom, bool trackChanges);
        void DeleteClassroomForFaculty(Guid facultyId, Guid id, bool trackChanges);
    }
}
