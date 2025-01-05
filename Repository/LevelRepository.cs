using Contracts;
using Entities.Models;

namespace Repository
{
    public class LevelRepository : RepositoryBase<Level>, ILevelRepository
    {
        public LevelRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<Level> GetAllLevels(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToList();
        public Level GetLevel(Guid levelId, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(levelId), trackChanges)
            .SingleOrDefault();
        public void CreateLevel(Level level) => Create(level);
    }
}
