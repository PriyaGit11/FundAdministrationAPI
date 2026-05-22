using FundAdministrationAPI.Models; 
 
namespace FundAdministrationAPI.Repositories.Interfaces 
{ 
    public interface ITransactionRepository 
    { 
        Task AddAsync(Transaction transaction); 
 
        Task<IEnumerable<Transaction>> GetTransactionsByInvestorAsync(Guid 
investorId); 
    } 
}