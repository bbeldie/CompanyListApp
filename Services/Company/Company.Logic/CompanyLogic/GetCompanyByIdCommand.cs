using Company.Models.Entities;
using MediatR;


namespace Company.Logic.CompanyLogic
{
    public class GetCompanyByIdCommand : IRequest<CompanyModel>
    {
        public int Id { get; set; }

        public GetCompanyByIdCommand(int id)
        {
            Id = id;
        }
    }
}
