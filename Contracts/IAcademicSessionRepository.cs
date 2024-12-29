using Entities.Models;

namespace Contracts
{
    public interface IAcademicSessionRepository
    {
        IEnumerable<AcademicSession> GetAllAcademicSessions(bool trackChanges);
        AcademicSession GetAcademicSession(Guid sessionId, bool trackChanges);
        void CreateAcademicSession(AcademicSession session);
        void DeleteAcademicSession(AcademicSession session);
    }
}
