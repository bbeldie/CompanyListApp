using Company.Models.Entities;
using MediatR;

namespace Company.Logic.CompanyLogic
{
    public class GetFilteredCompaniesQuery : IRequest<List<CompanyModel>>
    {
        public string? Name { get; set; }
        public string? Ticker { get; set; }
        public string? Exchange { get; set; }
        public string? Isin { get; set; }
        public string? Website { get; set; }
    }
}
