using FundAdministrationAPI.Data; 
using FundAdministrationAPI.Models; 
using FundAdministrationAPI.Repositories.Interfaces; 
using Microsoft.EntityFrameworkCore; 
 
namespace FundAdministrationAPI.Repositories 
{ 
    public class InvestorRepository : IInvestorRepository 
    { 
        private readonly AppDbContext _context; 
 
        public InvestorRepository(AppDbContext context) 
        { 
            _context = context; 
        } 
 
        public async Task<IEnumerable<Investor>> GetAllAsync() 
        { 
            return await _context.Investors.Include(x => 
x.Fund).ToListAsync(); 
        } 
 
        public async Task<Investor?> GetByIdAsync(Guid id) 
        { 
            return await _context.Investors.FindAsync(id); 
        } 
 
        public async Task AddAsync(Investor investor) 
        { 
            await _context.Investors.AddAsync(investor); 
            await _context.SaveChangesAsync(); 
        } 
 
        public async Task UpdateAsync(Investor investor) 
        { 
            _context.Investors.Update(investor); 
            await _context.SaveChangesAsync(); 
        } 
 
        public async Task DeleteAsync(Guid id) 
        { 
            var investor = await _context.Investors.FindAsync(id); 
 
            if (investor != null) 
            { 
                _context.Investors.Remove(investor); 
                await _context.SaveChangesAsync(); 
            } 
        } 
    } 
}