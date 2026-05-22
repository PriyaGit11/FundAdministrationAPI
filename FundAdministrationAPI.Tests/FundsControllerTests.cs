using FundAdministrationAPI.Controllers;
using FundAdministrationAPI.DTOs;
using FundAdministrationAPI.Models;
using FundAdministrationAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FundAdministrationAPI.Tests
{
    public class FundsControllerTests
    {
        private readonly Mock<IFundRepository> _fundRepositoryMock;
        private readonly Mock<ILogger<FundsController>> _loggerMock;
        private readonly FundsController _controller;

        public FundsControllerTests()
        {
            _fundRepositoryMock = new Mock<IFundRepository>();

            _loggerMock = new Mock<ILogger<FundsController>>();

            _controller = new FundsController(
                _fundRepositoryMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task GetAllFunds_Returns_OkResult()
        {
            // Arrange
            var funds = new List<Fund>
            {
                new Fund
                {
                    FundId = Guid.NewGuid(),
                    Name = "Equity Fund",
                    Currency = "USD",
                    LaunchDate = DateTime.UtcNow
                }
            };

            _fundRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(funds);

            // Act
            var result = await _controller.GetAllFunds();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnFunds =
                Assert.IsAssignableFrom<IEnumerable<Fund>>(okResult.Value);

            Assert.Single(returnFunds);
        }

        [Fact]
        public async Task GetFundById_Returns_NotFound()
        {
            // Arrange
            _fundRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Fund?)null);

            // Act
            var result = await _controller.GetFundById(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateFund_Returns_OkResult()
        {
            // Arrange
            var dto = new CreateFundDto
            {
                Name = "New Fund",
                Currency = "EUR",
                LaunchDate = DateTime.UtcNow
            };

            // Act
            var result = await _controller.CreateFund(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var fund = Assert.IsType<Fund>(okResult.Value);

            Assert.Equal(dto.Name, fund.Name);
        }

        [Fact]
        public async Task DeleteFund_Returns_NoContent()
        {
            // Act
            var result = await _controller.DeleteFund(Guid.NewGuid());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}