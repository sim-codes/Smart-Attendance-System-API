using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IFacultyRepository Faculty { get; }
        IDepartmentRepository Department { get; }
        IClassroomRepository Classroom { get; }
        void Save();
    }
}
