using Company.Dal;
using Company.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Company.Logic.CompanyLogic
{
    public class UpdateCompanyCommandHandler(CompanyDbContext dbContext) : IRequestHandler<UpdateCompanyCommand, CompanyModel>
    {

        public async Task<CompanyModel> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await dbContext.Companies.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (company == null)
            {
                throw new KeyNotFoundException($"Company with ID {request.Id} not found.");
            }

            // Validate ISIN format if updated
            if (!string.IsNullOrWhiteSpace(request.Isin))
            {
                if (!Regex.IsMatch(request.Isin, @"^[A-Za-z]{2}[A-Za-z0-9]{10}$"))
                {
                    throw new ArgumentException("Invalid ISIN format. It must start with two letters followed by 10 alphanumeric characters.");
                }

                if (await dbContext.Companies.AnyAsync(c => c.Isin == request.Isin && c.Id != request.Id, cancellationToken))
                {
                    throw new InvalidOperationException("A company with the same ISIN already exists.");
                }
            }

            company.Name = request.Name ?? company.Name;
            company.Ticker = request.Ticker ?? company.Ticker;
            company.Exchange = request.Exchange ?? company.Exchange;
            company.Isin = request.Isin ?? company.Isin;
            company.Website = request.Website ?? company.Website;

            dbContext.Companies.Update(company);
            await dbContext.SaveChangesAsync(cancellationToken);

            return company;
        }
    }
}
