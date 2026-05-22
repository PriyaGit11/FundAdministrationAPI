using FundAdministrationAPI.Data; 
using FundAdministrationAPI.Models; 
using FundAdministrationAPI.Repositories.Interfaces; 
using Microsoft.EntityFrameworkCore; 
 
namespace FundAdministrationAPI.Repositories 
{ 
    public class FundRepository : IFundRepository 
    { 
        private readonly AppDbContext _context; 
 
        public FundRepository(AppDbContext context) 
        { 
            _context = context; 
        } 
 
        public async Task<IEnumerable<Fund>> GetAllAsync() 
        { 
            return await _context.Funds.ToListAsync(); 
        } 
 
        public async Task<Fund?> GetByIdAsync(Guid id) 
        { 
            return await _context.Funds.FindAsync(id); 
        } 
 
        public async Task AddAsync(Fund fund) 
        { 
            await _context.Funds.AddAsync(fund); 
            await _context.SaveChangesAsync(); 
        } 
 
        public async Task UpdateAsync(Fund fund) 
        { 
            _context.Funds.Update(fund); 
            await _context.SaveChangesAsync(); 
        } 
 
        public async Task DeleteAsync(Guid id) 
        { 
            var fund = await _context.Funds.FindAsync(id); 
 
            if (fund != null) 
            { 
                _context.Funds.Remove(fund); 
                await _context.SaveChangesAsync(); 
            } 
        } 
    } 
}