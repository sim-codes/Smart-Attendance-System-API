using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class AcademicSessionService : IAcademicSessionService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public AcademicSessionService(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<AcademicSessionDto> GetAllAcademicSessions(bool trackChanges)
        {
            var academicSessions = _repository.AcademicSession.GetAllAcademicSessions(trackChanges);
            var academicSessionsDto = _mapper.Map<IEnumerable<AcademicSessionDto>>(academicSessions);
            return academicSessionsDto;
        }

        public AcademicSessionDto GetAcademicSession(Guid academicSessionId, bool trackChanges)
        {
            var academicSession = _repository.AcademicSession.GetAcademicSession(academicSessionId, trackChanges);
            if (academicSession is null)
                throw new AcademicSessionNotFoundException(academicSessionId);
            var academicSessionDto = _mapper.Map<AcademicSessionDto>(academicSession);
            return academicSessionDto;
        }

        public AcademicSessionDto CreateAcademicSession(AcademicSessionForCreationDto academicSession)
        {
            var academicSessionEntity = _mapper.Map<AcademicSession>(academicSession);
            _repository.AcademicSession.CreateAcademicSession(academicSessionEntity);
            _repository.Save();
            var academicSessionToReturn = _mapper.Map<AcademicSessionDto>(academicSessionEntity);
            return academicSessionToReturn;
        }

        public void DeleteAcademicSession(Guid academicSessionId, bool trackChanges)
        {
            var academicSession = _repository.AcademicSession.GetAcademicSession(academicSessionId, trackChanges);
            if (academicSession is null)
                throw new AcademicSessionNotFoundException(academicSessionId);
            _repository.AcademicSession.DeleteAcademicSession(academicSession);
            _repository.Save();
        }
    }
}
