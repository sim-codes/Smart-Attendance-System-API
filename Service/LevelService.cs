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
    internal sealed class LevelService : ILevelService
    {
        private IRepositoryManager _repository;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public LevelService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<LevelDto> GetAllLevels(bool trackChanges)
        {
            var levels = _repository.Level.GetAllLevels(trackChanges);
            var levelsDto = _mapper.Map<IEnumerable<LevelDto>>(levels);
            return levelsDto;
        }

        public LevelDto GetLevel(Guid id, bool trackChanges)
        {
            var level = _repository.Level.GetLevel(id, trackChanges);
            if (level is null)
                throw new LevelNotFoundException(id);
            var levelDto = _mapper.Map<LevelDto>(level);
            return levelDto;
        }

        public LevelDto CreateLevel(LevelForCreationDto level)
        {
            var levelEntity = _mapper.Map<Level>(level);
            _repository.Level.CreateLevel(levelEntity);
            _repository.Save();
            var levelToReturn = _mapper.Map<LevelDto>(levelEntity);
            return levelToReturn;
        }
    }
}
