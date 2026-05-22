using System.ComponentModel.DataAnnotations; 
using FundAdministrationAPI.Models; 
 
namespace FundAdministrationAPI.DTOs 
{ 
    public class CreateTransactionDto 
    { 
        [Required] 
        public Guid InvestorId { get; set; } 
 
        [Required] 
        public TransactionType Type { get; set; } 
 
        [Range(0.01, double.MaxValue, 
            ErrorMessage = "Amount must be greater than zero")] 
        public decimal Amount { get; set; } 
 
        public DateTime TransactionDate { get; set; } 
    } 
}