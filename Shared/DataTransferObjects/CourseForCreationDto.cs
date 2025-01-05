using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record CourseForCreationDto(string Title, string Code, string Description, int CreditUnits, Guid LevelId);
}
