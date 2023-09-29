using CodeChallenge.DTO;
using CodeChallenge.Models;

namespace CodeChallenge.Helpers
{
    public interface IMapper
    {
        Employee EmployeeDTO_To_Employee(EmployeeDTO employeeDto);
        Compensation CompensationDTO_To_Compensation(CompensationDTO compensationDto);
    }
}
