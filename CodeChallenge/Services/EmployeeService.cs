using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;
using CodeChallenge.Helpers;
using CodeChallenge.DTO;

namespace CodeChallenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;
        private readonly IMapper _mapper;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public Employee Create(EmployeeDTO employeeDTO)
        {
            if(employeeDTO != null)
            {
                var newEmployee = _employeeRepository.Add(_mapper.EmployeeDTO_To_Employee(employeeDTO));
                _employeeRepository.SaveAsync().Wait();
                return newEmployee;
            }

            return null;
        }

        public Employee GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }

        public ReportingStructure GetReportingStructureById(string id)
        {
            _logger.LogDebug($"Getting reporting structure of {id}");
            var employee = GetById(id);
            if(employee == null)
            {
                return null;
            }

            return new ReportingStructure()
            {
                Employee = employee,
                NumberOfReports = calculateNumberOfDirectReports(employee)
            };
        }

        private int calculateNumberOfDirectReports(Employee employee)
        {
            if(employee.DirectReports?.Any() != true)
            {
                return 0;
            }
            var value = employee.DirectReports.Count();
            foreach(var emp in employee.DirectReports ) 
            {
                var fullEmployee = GetById(emp.EmployeeId); 
                if (emp != null)
                {
                    value += calculateNumberOfDirectReports(fullEmployee);
                }
            }

            return value;
        }
    }
}
