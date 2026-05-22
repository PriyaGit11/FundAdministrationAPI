using FundAdministrationAPI.Models; 
using Microsoft.EntityFrameworkCore; 
 
namespace FundAdministrationAPI.Data 
{ 
    public class AppDbContext : DbContext 
    { 
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) 
        { 
        } 
 
        public DbSet<Fund> Funds { get; set; } 
 
        public DbSet<Investor> Investors { get; set; } 
 
        public DbSet<Transaction> Transactions { get; set; } 
    } 
} 