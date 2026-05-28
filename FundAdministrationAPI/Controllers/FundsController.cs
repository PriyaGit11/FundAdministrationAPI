using FundAdministrationAPI.DTOs; 
using FundAdministrationAPI.Models; 
using FundAdministrationAPI.Repositories.Interfaces; 
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 
 
namespace FundAdministrationAPI.Controllers 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    [Authorize] 
    public class FundsController : ControllerBase 
    { 
        private readonly IFundRepository _fundRepository; 
        private readonly ILogger<FundsController> _logger;
 
        public FundsController(IFundRepository fundRepository, ILogger<FundsController> logger)
        {
            _fundRepository = fundRepository;
            _logger = logger;
        }
 
        [HttpGet("GetAllFunds")]
        public async Task<IActionResult> GetAllFunds() 
        { 
            _logger.LogInformation("Fetching all funds");
            var funds = await _fundRepository.GetAllAsync(); 
            _logger.LogInformation("Fetching completed for all funds");
            //throw new Exception("Test Exception");
            return Ok(funds); 
        } 
 
        [HttpGet("GetFundById/{id}")] 
        public async Task<IActionResult> GetFundById(Guid id) 
        { 
            _logger.LogInformation("Fetching funds for funds Id");
            var fund = await _fundRepository.GetByIdAsync(id); 
 
            if (fund == null) 
            { 
                return NotFound(); 
            } 
 
            return Ok(fund); 
        } 
 
        [HttpPost("CreateFund")]
        public async Task<IActionResult> CreateFund(CreateFundDto dto) 
        { 
            _logger.LogInformation("Create funds record");
            if (dto.LaunchDate > DateTime.UtcNow)
            {
                return BadRequest("Launch date cannot be future date");
            }
            var fund = new Fund 
            { 
                FundId = Guid.NewGuid(), 
                Name = dto.Name, 
                Currency = dto.Currency, 
                LaunchDate = dto.LaunchDate 
            }; 
 
            await _fundRepository.AddAsync(fund); 
            _logger.LogInformation("Funds record created successfully");
            return Ok(fund); 
        } 
 
        [HttpPut("UpdateFund/{id}")] 
        public async Task<IActionResult> UpdateFund(Guid id, CreateFundDto dto) 
        { 
            _logger.LogInformation("Update funds record for funds Id");
            var fund = await _fundRepository.GetByIdAsync(id); 
 
            if (fund == null) 
            { 
                return NotFound(); 
            } 
 
            fund.Name = dto.Name; 
            fund.Currency = dto.Currency; 
            fund.LaunchDate = dto.LaunchDate; 
 
            await _fundRepository.UpdateAsync(fund); 
            _logger.LogInformation("Update completed");
            return Ok(fund); 
        } 
 
        [HttpDelete("DeleteFund/{id}")] 
        public async Task<IActionResult> DeleteFund(Guid id) 
        { 
            _logger.LogInformation("Delete funds record");
            await _fundRepository.DeleteAsync(id); 
 
            return Ok("Funds record deleted"); 
        } 
    } 
}