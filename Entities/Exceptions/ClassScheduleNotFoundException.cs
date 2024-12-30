using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ClassScheduleNotFoundException : NotFoundException
    {
        public ClassScheduleNotFoundException(Guid Id)
            : base($"The Class schedule with Id: {Id} is does not exist in the database")
        { }
    }
}
