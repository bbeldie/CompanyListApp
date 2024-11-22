using Company.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Company.Logic.CompanyLogic
{
    public class CreateCompanyCommand : IRequest<CompanyModel>
    {
        public string? Name { get; set; }
        public string? Exchange { get; set; }
        public string? Ticker { get; set; }
        public string? Isin { get; set; }
        public string? Website { get; set; }
    }
}
