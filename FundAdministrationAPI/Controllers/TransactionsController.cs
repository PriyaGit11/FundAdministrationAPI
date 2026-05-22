using FundAdministrationAPI.Data; 
using FundAdministrationAPI.DTOs; 
using FundAdministrationAPI.Models; 
using FundAdministrationAPI.Repositories.Interfaces; 
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 
 
namespace FundAdministrationAPI.Controllers 
{ 
    [Route("api/[controller]")] 
    [ApiController] 
    [Authorize] 
    public class TransactionsController : ControllerBase 
    { 
        private readonly ITransactionRepository _transactionRepository; 
        private readonly AppDbContext _context; 
 
        public TransactionsController( 
            ITransactionRepository transactionRepository, 
            AppDbContext context) 
        { 
            _transactionRepository = transactionRepository; 
            _context = context; 
        } 
 
        [HttpPost("CreateTransaction")] 
        public async Task<IActionResult> CreateTransaction(CreateTransactionDto dto) 
        { 
            var transaction = new Transaction 
            { 
                TransactionId = Guid.NewGuid(), 
                InvestorId = dto.InvestorId, 
                Type = dto.Type, 
                Amount = dto.Amount, 
                TransactionDate = dto.TransactionDate 
            }; 
 
            await _transactionRepository.AddAsync(transaction); 
 
            return Ok(transaction); 
        } 
 
        [HttpGet("GetTransactionsByInvestor/{investorId}")] 
        public async Task<IActionResult> GetTransactionsByInvestor(Guid 
investorId) 
        { 
            var result = await _transactionRepository 
                .GetTransactionsByInvestorAsync(investorId); 
 
            return Ok(result); 
        } 
 
        [HttpGet("GetFundSummary")] 
        public async Task<IActionResult> GetFundSummary() 
        { 
            var result = await _context.Transactions 
                .Include(x => x.Investor) 
                .GroupBy(x => x.Investor!.FundId) 
                .Select(g => new 
                { 
                    FundId = g.Key, 
 
                    TotalSubscribed = g 
                        .Where(x => x.Type == TransactionType.Subscription) 
                        .Sum(x => x.Amount), 
 
                    TotalRedeemed = g 
                        .Where(x => x.Type == TransactionType.Redemption) 
                        .Sum(x => x.Amount), 
 
                    NetInvestment = 
                        g.Where(x => x.Type == TransactionType.Subscription) 
                         .Sum(x => x.Amount) 
 
                        - 
 
                        g.Where(x => x.Type == TransactionType.Redemption) 
                         .Sum(x => x.Amount), 
 
                    InvestorCount = g 
                        .Select(x => x.InvestorId) 
                        .Distinct() 
                        .Count() 
                }) 
                .ToListAsync(); 
 
            return Ok(result); 
        } 
    } 
}