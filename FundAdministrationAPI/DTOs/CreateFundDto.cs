using System.ComponentModel.DataAnnotations; 
 
namespace FundAdministrationAPI.DTOs 
{ 
    public class CreateFundDto 
    { 
        [Required] 
        public string Name { get; set; } = string.Empty; 
 
        [Required] 
        public string Currency { get; set; } = string.Empty; 
 
        public DateTime LaunchDate { get; set; } 
    } 
}