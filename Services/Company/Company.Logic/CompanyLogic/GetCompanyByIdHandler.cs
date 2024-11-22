using Company.Dal;
using Company.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Company.Logic.CompanyLogic
{
    public class GetCompanyByIdHandler(CompanyDbContext dbContext) : IRequestHandler<GetCompanyByIdCommand, CompanyModel>
    {
        public async Task<CompanyModel> Handle(GetCompanyByIdCommand request, CancellationToken cancellationToken)
        {
            var company = await dbContext.Companies.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (company == null)
            {
                throw new KeyNotFoundException($"Company with ID {request.Id} not found.");
            }

            return company;
        }
    }
}
