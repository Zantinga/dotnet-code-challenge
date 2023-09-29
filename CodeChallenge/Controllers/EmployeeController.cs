using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;
using CodeChallenge.DTO;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            _logger.LogDebug($"Received employee create request for '{employeeDTO.FirstName} {employeeDTO.LastName}'");

            Employee newEmployee = _employeeService.Create(employeeDTO);

            return CreatedAtRoute("getEmployeeById", new { id = newEmployee.EmployeeId }, newEmployee);
        }

        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(String id, [FromBody]Employee newEmployee)
        {
            _logger.LogDebug($"Recieved employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById(id);
            if (existingEmployee == null)
                return NotFound();

            _employeeService.Replace(existingEmployee, newEmployee);

            return Ok(newEmployee);
        }

        [HttpGet("{id}/report-structure", Name = "getReportStructureById")]
        public IActionResult GetReportingStructureById(String id)
        {
            _logger.LogDebug($"Received reporting structure get request for '{id}'");

            var reportStructure = _employeeService.GetReportingStructureById(id);
            if (reportStructure == null)
                return NotFound();

            return Ok(reportStructure);
        }
    }
}
