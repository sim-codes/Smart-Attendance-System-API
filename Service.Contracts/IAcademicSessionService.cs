using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IAcademicSessionService
    {
        IEnumerable<AcademicSessionDto> GetAllAcademicSessions(bool trackChanges);
        AcademicSessionDto GetAcademicSession(Guid academicSessionId, bool trackChanges);
        AcademicSessionDto CreateAcademicSession(AcademicSessionForCreationDto academicSession);
        void DeleteAcademicSession(Guid academicSessionId, bool trackChanges);

    }
}
