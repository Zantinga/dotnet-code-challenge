using CodeChallenge.DTO;
using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Services
{
    public interface IEmployeeService
    {
        Employee GetById(string id);
        Employee Create(EmployeeDTO employeeDTO);
        Employee Replace(Employee originalEmployee, Employee newEmployee);
        ReportingStructure GetReportingStructureById(string id);
    }
}
