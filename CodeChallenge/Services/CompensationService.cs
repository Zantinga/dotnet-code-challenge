using System;
using CodeChallenge.DTO;
using CodeChallenge.Helpers;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;
        private readonly IMapper _mapper;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository, IMapper mapper)
        {
            _logger = logger;
            _compensationRepository = compensationRepository;
            _mapper = mapper;
        }

        public Compensation Create(CompensationDTO compensationDTO)
        {
            if (compensationDTO != null) 
            {
                _logger.LogDebug($"Creating compensation for employee {compensationDTO.EmployeeId}");
                var compensation = _mapper.CompensationDTO_To_Compensation(compensationDTO);
                if (compensation == null) 
                {
                    return null;
                }
                var newCompensation = _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync();
                return newCompensation;
            }
            return null;
        }

        public Compensation GetByEmployeeId(string id)
        {
            if(!string.IsNullOrEmpty(id))
            {
                _logger.LogDebug($"Getting compensation for employee {id}");
                return _compensationRepository.GetByEmployeeId(id);
            }

            return null;
        }
    }
}
