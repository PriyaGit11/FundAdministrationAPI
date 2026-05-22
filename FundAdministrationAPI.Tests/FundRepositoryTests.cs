using FundAdministrationAPI.Data;
using FundAdministrationAPI.Models;
using FundAdministrationAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FundAdministrationAPI.Tests
{
    public class FundRepositoryTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetAllAsync_Returns_AllFunds()
        {
            // Arrange
            var context = GetDbContext();

            context.Funds.Add(new Fund
            {
                FundId = Guid.NewGuid(),
                Name = "Fund 1",
                Currency = "USD",
                LaunchDate = DateTime.UtcNow
            });

            context.Funds.Add(new Fund
            {
                FundId = Guid.NewGuid(),
                Name = "Fund 2",
                Currency = "EUR",
                LaunchDate = DateTime.UtcNow
            });

            await context.SaveChangesAsync();

            var repository = new FundRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_Returns_Fund_WhenExists()
        {
            // Arrange
            var context = GetDbContext();

            var fundId = Guid.NewGuid();

            context.Funds.Add(new Fund
            {
                FundId = fundId,
                Name = "Equity Fund",
                Currency = "USD",
                LaunchDate = DateTime.UtcNow
            });

            await context.SaveChangesAsync();

            var repository = new FundRepository(context);

            // Act
            var result = await repository.GetByIdAsync(fundId);

            // Assert
            Assert.NotNull(result);

            Assert.Equal("Equity Fund", result.Name);
        }

        [Fact]
        public async Task AddAsync_Adds_Fund()
        {
            // Arrange
            var context = GetDbContext();

            var repository = new FundRepository(context);

            var fund = new Fund
            {
                FundId = Guid.NewGuid(),
                Name = "New Fund",
                Currency = "USD",
                LaunchDate = DateTime.UtcNow
            };

            // Act
            await repository.AddAsync(fund);

            var result = await context.Funds.FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(result);

            Assert.Equal("New Fund", result.Name);
        }

        [Fact]
        public async Task UpdateAsync_Updates_Fund()
        {
            // Arrange
            var context = GetDbContext();

            var fund = new Fund
            {
                FundId = Guid.NewGuid(),
                Name = "Old Fund",
                Currency = "USD",
                LaunchDate = DateTime.UtcNow
            };

            context.Funds.Add(fund);

            await context.SaveChangesAsync();

            var repository = new FundRepository(context);

            // Act
            fund.Name = "Updated Fund";

            await repository.UpdateAsync(fund);

            var result = await context.Funds.FindAsync(fund.FundId);

            // Assert
            Assert.NotNull(result);

            Assert.Equal("Updated Fund", result.Name);
        }

        [Fact]
        public async Task DeleteAsync_Removes_Fund()
        {
            // Arrange
            var context = GetDbContext();

            var fund = new Fund
            {
                FundId = Guid.NewGuid(),
                Name = "Delete Fund",
                Currency = "USD",
                LaunchDate = DateTime.UtcNow
            };

            context.Funds.Add(fund);

            await context.SaveChangesAsync();

            var repository = new FundRepository(context);

            // Act
            await repository.DeleteAsync(fund.FundId);

            var result = await context.Funds.FindAsync(fund.FundId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_DoesNothing_WhenFundNotFound()
        {
            // Arrange
            var context = GetDbContext();

            var repository = new FundRepository(context);

            // Act
            await repository.DeleteAsync(Guid.NewGuid());

            // Assert
            var funds = await context.Funds.ToListAsync();

            Assert.Empty(funds);
        }
    }
}