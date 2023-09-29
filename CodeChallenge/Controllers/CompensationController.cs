using CodeChallenge.DTO;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
    [Route("api/compensation")]
    [ApiController]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger<CompensationController> _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] CompensationDTO compensationDTO)
        {
            _logger.LogDebug($"Received compensation create request for employee: '{compensationDTO.EmployeeId}");

            if(compensationDTO.EmployeeId == null)
            {
                return BadRequest();
            }
            Compensation newCompensation = _compensationService.Create(compensationDTO);

            if(newCompensation == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("getCompensationByEmployeeId", new { id = compensationDTO.EmployeeId }, newCompensation);
        }

        [HttpGet("{id}", Name = "getCompensationByEmployeeId")]
        public IActionResult GetCompensation(string id) 
        {
            _logger.LogDebug($"Received compensation get request for employee '{id}'");

            var compensation = _compensationService.GetByEmployeeId(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }
    }
}
