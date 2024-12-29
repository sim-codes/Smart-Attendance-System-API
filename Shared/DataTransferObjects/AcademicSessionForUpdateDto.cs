using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record AcademicSessionForUpdateDto(string Name, DateOnly StartDate, DateOnly EndDate, bool IsActive);
}
