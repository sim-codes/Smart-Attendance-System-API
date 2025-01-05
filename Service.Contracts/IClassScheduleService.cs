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
        IEnumerable<ClassScheduleDto> GetClassSchedules(bool trackChanges);
        ClassScheduleDto GetClassScheduleById(Guid Id, bool trackChanges);
        ClassScheduleDto CreateClassSchedule(ClassScheduleForCreationDto classSchedule);
        void UpdateClassSchedule(Guid Id, ClassScheduleForUpdateDto classSchedule, bool trackChanges);
    }
}
