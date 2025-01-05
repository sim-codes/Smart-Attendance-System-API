using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class LevelNotFoundException : NotFoundException
    {
        public LevelNotFoundException(Guid levelId)
            : base($"The Level with id: {levelId} doesn't exist in the database.")
        {
        }
    }
}
