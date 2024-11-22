using Company.Dal;
using Company.Logic.CompanyLogic;
using Company.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace Company.Tests.Logic
{
    public class CreateCompanyHandlerTests
    {

        private readonly DbContextOptions<CompanyDbContext> _dbContextOptions;

        public CreateCompanyHandlerTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<CompanyDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }

        [Fact]
        public async Task Handle_ShouldCreateCompany_WhenValidRequest()
        {
            // Arrange
            using var dbContext = new CompanyDbContext(_dbContextOptions);

            var handler = new CreateCompanyHandler(dbContext);

            var request = new CreateCompanyCommand
            {
                Name = "Test Company",
                Ticker = "TST",
                Exchange = "NYSE",
                Isin = "US1234567891",
                Website = "https://test.com"
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Company", result.Name);
            Assert.Equal("US1234567891", result.Isin);
        }

        [Fact]
        public async Task Handle_ShouldThrowInvalidOperationException_WhenDuplicateIsin()
        {
            // Arrange
            using var dbContext = new CompanyDbContext(_dbContextOptions);
            dbContext.Companies.Add(new CompanyModel
            {
                Id = 1,
                Name = "Existing Company",
                Ticker = "EXST",
                Exchange = "NASDAQ",
                Isin = "US1234567890"
            });
            await dbContext.SaveChangesAsync();

            var handler = new CreateCompanyHandler(dbContext);

            var request = new CreateCompanyCommand
            {
                Name = "Test Company",
                Ticker = "TST",
                Exchange = "NYSE",
                Isin = "US1234567890",
                Website = "https://test.com"
            };

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(request, CancellationToken.None));

        }
    }
}
