using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ILevelRepository
    {
        IEnumerable<Level> GetAllLevels(bool trackChanges);
        Level GetLevel(Guid levelId, bool trackChanges);
        void CreateLevel(Level level);
    }
}
