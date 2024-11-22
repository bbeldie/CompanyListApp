using Company.Models.Entities;
using MediatR;

namespace Company.Logic.CompanyLogic
{
    public class UpdateCompanyCommand : IRequest<CompanyModel>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Ticker { get; set; }
        public string? Exchange { get; set; }
        public string? Isin { get; set; }
        public string? Website { get; set; }
    }
}
