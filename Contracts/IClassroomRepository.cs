using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IClassroomRepository
    {
        IEnumerable<Classroom> GetClassrooms(Guid facultyId, bool trackChanges);
        Classroom GetClassroom(Guid facultyId, Guid id, bool trackChanges);
        void CreateClassroom(Guid facultyId, Classroom classroom);
        void DeleteClassroom(Classroom classroom);
    }
}
