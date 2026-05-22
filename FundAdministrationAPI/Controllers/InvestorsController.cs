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
    public class InvestorsController : ControllerBase 
    { 
        private readonly IInvestorRepository _investorRepository; 
 
        public InvestorsController(IInvestorRepository investorRepository) 
        { 
            _investorRepository = investorRepository; 
        } 
 
        [HttpGet("GetAllInvestors")] 
        public async Task<IActionResult> GetAllInvestors() 
        { 
            var investors = await _investorRepository.GetAllAsync(); 
 
            return Ok(investors); 
        } 
 
        [HttpPost("CreateInvestors")] 
        public async Task<IActionResult> CreateInvestors(CreateInvestorDto dto) 
        { 
            var investor = new Investor 
            { 
                InvestorId = Guid.NewGuid(), 
                FullName = dto.FullName, 
                Email = dto.Email, 
                FundId = dto.FundId 
            }; 
 
            await _investorRepository.AddAsync(investor); 
 
            return Ok(investor); 
        } 
    } 
}