using CodeChallenge.DTO;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace CodeChallenge.Helpers
{
    public class Mapper : IMapper
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<Mapper> _logger;

        public Mapper(ILogger<Mapper> logger, IEmployeeRepository employeeRepository, ICompensationRepository compensationRepository)
        {
            _employeeRepository = employeeRepository;
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        public Employee EmployeeDTO_To_Employee(EmployeeDTO employeeDTO)
        {
            // Create employee without DirectReports
            var newEmployee = new Employee()
            {
                FirstName = employeeDTO.FirstName,
                LastName = employeeDTO.LastName,
                Position = employeeDTO.Position,
                Department = employeeDTO.Department,
            };

            // Add DirectReports
            if(employeeDTO.DirectReports != null)
            {
                var directReports = new List<Employee>();

                foreach(var employeeId in employeeDTO.DirectReports)
                {
                    var employeeData = _employeeRepository.GetById(employeeId);

                    if(employeeData != null)
                    {
                        directReports.Add(employeeData);
                    }
                }
                newEmployee.DirectReports = directReports;
            }
            return newEmployee;
        }
        public Compensation CompensationDTO_To_Compensation(CompensationDTO compensationDTO)
        {
            var employee = _employeeRepository.GetById(compensationDTO.EmployeeId);
            if(employee == null) 
            {
                return null;
            }
            return new Compensation()
            {
                Employee = employee,
                EffectiveDate = compensationDTO.EffectiveDate,
                Salary = compensationDTO.Salary
            };

        }
    }
}
