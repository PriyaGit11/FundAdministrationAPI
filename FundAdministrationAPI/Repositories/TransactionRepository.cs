using FundAdministrationAPI.Data; 
using FundAdministrationAPI.Models; 
using FundAdministrationAPI.Repositories.Interfaces; 
using Microsoft.EntityFrameworkCore; 
 
namespace FundAdministrationAPI.Repositories 
{ 
    public class TransactionRepository : ITransactionRepository 
    { 
        private readonly AppDbContext _context; 
 
        public TransactionRepository(AppDbContext context) 
        { 
            _context = context; 
        } 
 
        public async Task AddAsync(Transaction transaction) 
        { 
            await _context.Transactions.AddAsync(transaction); 
            await _context.SaveChangesAsync(); 
        } 
 
        public async Task<IEnumerable<Transaction>> 
GetTransactionsByInvestorAsync(Guid investorId) 
        { 
            return await _context.Transactions 
                .Where(x => x.InvestorId == investorId) 
                .ToListAsync(); 
        } 
    } 
}