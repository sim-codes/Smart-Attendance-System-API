using Shared;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IClassScheduleService
    {
        IEnumerable<ClassScheduleDto> GetClassSchedules();
        ClassScheduleDto GetClassScheduleById(Guid Id, bool trackChanges);
        ClassroomDto CreateClassSchedule(ClassScheduleForCreationDto classSchedule);
    }
}
