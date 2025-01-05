using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ILevelService
    {
        IEnumerable<LevelDto> GetAllLevels(bool trackChanges);
        LevelDto GetLevel(Guid levelId, bool trackChanges);
        LevelDto CreateLevel(LevelForCreationDto level);
    }
}
