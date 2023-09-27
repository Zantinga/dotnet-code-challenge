﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

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
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            _logger.LogDebug($"Received employee create request for '{employee.FirstName} {employee.LastName}'");

            _employeeService.Create(employee);

            return CreatedAtRoute("getEmployeeById", new { id = employee.EmployeeId }, employee);
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
            _logger.LogDebug($"Recieved reporting structure get request for '{id}'");

            //var existingEmployee = _employeeService.GetById(id);
            //if (existingEmployee == null)
            //    return NotFound();

            //TODO: Implement ReportingStructure tree crawling

            var reportStructure = _employeeService.GetReportingStructureById(id);
            if (reportStructure == null)
                return NotFound();

            //TODO: Employee.DirectReports isn't working
            var employeeReport = new ReportingStructure
            {
                Employee = reportStructure.Employee,
                NumberOfReports = reportStructure.NumberOfReports
            };

            return Ok(employeeReport);
        }
    }
}
