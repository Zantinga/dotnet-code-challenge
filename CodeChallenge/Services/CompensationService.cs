using System;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _compensationRepository = compensationRepository;
            _employeeRepository = employeeRepository;
        }

        public Compensation Create(Compensation compensation)
        {
            if (compensation != null) 
            {
                _logger.LogDebug($"Creating compensation for employee {compensation.Employee.EmployeeId}");
                var employee = _employeeRepository.GetById(compensation.Employee.EmployeeId);
                if (employee == null) 
                {
                    return null;
                }
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync();
            }
            return compensation;
        }

        public Compensation GetCompensationByEmployeeId(string id)
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
