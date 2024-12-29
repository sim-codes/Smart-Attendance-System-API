using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AcademicSessionRepository : RepositoryBase<AcademicSession>, IAcademicSessionRepository
    {
        public AcademicSessionRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<AcademicSession> GetAllAcademicSessions(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.StartDate)
            .ToList();

        public AcademicSession GetAcademicSession(Guid sessionId, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(sessionId), trackChanges)
            .SingleOrDefault();

        public void CreateAcademicSession(AcademicSession session) => Create(session);

        public void DeleteAcademicSession(AcademicSession session) => Delete(session);
    }
}
