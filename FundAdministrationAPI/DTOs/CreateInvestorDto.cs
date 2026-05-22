using System.ComponentModel.DataAnnotations; 
 
namespace FundAdministrationAPI.DTOs 
{ 
    public class CreateInvestorDto 
    { 
        [Required] 
        public string FullName { get; set; } = string.Empty; 
 
        [Required] 
        [EmailAddress] 
        public string Email { get; set; } = string.Empty; 
 
        [Required] 
        public Guid FundId { get; set; } 
    } 
}