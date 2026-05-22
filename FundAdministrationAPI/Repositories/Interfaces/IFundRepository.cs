using FundAdministrationAPI.Models; 
 
namespace FundAdministrationAPI.Repositories.Interfaces 
{ 
    public interface IFundRepository 
    { 
        Task<IEnumerable<Fund>> GetAllAsync(); 
 
        Task<Fund?> GetByIdAsync(Guid id); 
 
        Task AddAsync(Fund fund); 
 
        Task UpdateAsync(Fund fund); 
 
        Task DeleteAsync(Guid id); 
    } 
} 