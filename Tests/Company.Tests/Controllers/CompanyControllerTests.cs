using Company.Logic.CompanyLogic;
using Company.Models.Entities;
using Company.RestApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Company.Tests.Controllers
{
    public class CompanyControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CompanyController _controller;

        public CompanyControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new CompanyController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetCompanyById_ShouldReturnOk_WhenCompanyExists()
        {
            // Arrange
            var companyId = 1;
            var mockCompany = new CompanyModel { Id = companyId, Name = "Test", Ticker = "TEST", Exchange = "Test", Isin = "XX00000000000" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCompanyByIdCommand>(), default)).ReturnsAsync(mockCompany);

            // Act
            var result = await _controller.GetCompanyById(companyId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(mockCompany, okResult.Value);
        }

        [Fact]
        public async Task GetCompanyById_ShouldReturnNotFound_WhenCompanyDoesNotExist()
        {
            // Arrange
            var companyId = 1;
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCompanyByIdCommand>(), default)).Throws<KeyNotFoundException>();

            // Act
            var result = await _controller.GetCompanyById(companyId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task AddCompany_ShouldReturnCreated_WhenCompanyIsAddedSuccessfully()
        {
            // Arrange
            var createCommand = new CreateCompanyCommand { Name = "Test", Ticker = "TEST" };
            var createdCompany = new CompanyModel { Id = 1, Name = "Test", Ticker = "TEST", Exchange = "Test", Isin = "XX00000000000" };
            _mediatorMock.Setup(m => m.Send(createCommand, default)).ReturnsAsync(createdCompany);

            // Act
            var result = await _controller.AddCompany(createCommand);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(createdCompany, createdResult.Value);
        }
    }
}
