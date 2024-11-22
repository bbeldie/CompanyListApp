using Company.Dal;
using Company.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Company.Logic.CompanyLogic
{
    public class CreateCompanyHandler(CompanyDbContext dbContext) : IRequestHandler<CreateCompanyCommand, CompanyModel>
    {

        public async Task<CompanyModel> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name) ||
            string.IsNullOrWhiteSpace(request.Ticker) ||
            string.IsNullOrWhiteSpace(request.Exchange) ||
            string.IsNullOrWhiteSpace(request.Isin))
            {
                throw new ArgumentException("All fields except Website are required.");
            }

            // Validate ISIN format
            if (!Regex.IsMatch(request.Isin, @"^[A-Za-z]{2}[A-Za-z0-9]{10}$"))
            {
                throw new ArgumentException("Invalid ISIN format. It must start with two letters followed by 10 alphanumeric characters.");
            }

            if (await dbContext.Companies.AnyAsync(c => c.Isin == request.Isin, cancellationToken))
            {
                throw new InvalidOperationException("A company with the same ISIN already exists.");
            }

            var newCompany = new CompanyModel
            {
                Name = request.Name,
                Ticker = request.Ticker,
                Exchange = request.Exchange,
                Isin = request.Isin,
                Website = request.Website
            };

            dbContext.Companies.Add(newCompany);
            await dbContext.SaveChangesAsync(cancellationToken);

            return newCompany;
        }
    }
}
