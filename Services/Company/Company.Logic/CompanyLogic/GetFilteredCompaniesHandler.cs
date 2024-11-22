using Company.Dal;
using Company.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Company.Logic.CompanyLogic
{
    public class GetFilteredCompaniesHandler(CompanyDbContext dbContext) : IRequestHandler<GetFilteredCompaniesQuery, List<CompanyModel>>
    {
        public async Task<List<CompanyModel>> Handle(GetFilteredCompaniesQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Companies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(c => EF.Functions.ILike(c.Name, $"%{request.Name}%"));
            }

            if (!string.IsNullOrWhiteSpace(request.Ticker))
            {
                query = query.Where(c => EF.Functions.ILike(c.Ticker, $"%{request.Ticker}%"));
            }

            if (!string.IsNullOrWhiteSpace(request.Exchange))
            {
                query = query.Where(c => EF.Functions.ILike(c.Exchange, $"%{request.Exchange}%"));
            }

            if (!string.IsNullOrWhiteSpace(request.Isin))
            {
                query = query.Where(c => EF.Functions.ILike(c.Isin, $"%{request.Isin}%"));
            }

            if (!string.IsNullOrWhiteSpace(request.Website))
            {
                query = query.Where(c => !string.IsNullOrEmpty(c.Website) && EF.Functions.ILike(c.Website, $"%{request.Website}%"));
            }

            return await query.OrderBy(x => x.Id).ToListAsync(cancellationToken);
        }
    }
}
