using CodeChallenge.DTO;
using CodeChallenge.Models;
using System;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        Compensation Create(CompensationDTO compensationDTO);
        Compensation GetByEmployeeId(string id);
    }
}