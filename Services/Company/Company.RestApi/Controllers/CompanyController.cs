using Company.Logic.CompanyLogic;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Company.RestApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController(IMediator mediator) : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var query = new GetCompanyByIdCommand(id);
            var company = await mediator.Send(query);
            return Ok(company);
        }

        [HttpGet("filtered")]
        public async Task<IActionResult> GetFilteredCompanies([FromQuery] GetFilteredCompaniesQuery query)
        {
            var companies = await mediator.Send(query);
            return Ok(companies);
        }


        [HttpPost]
        public async Task<IActionResult> AddCompany([FromBody] CreateCompanyCommand company)
        {
            var newCompany = await mediator.Send(company);
            return CreatedAtAction(nameof(GetCompanyById), new { id = newCompany.Id }, newCompany);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] UpdateCompanyCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("The ID in the URL must match the ID in the request body.");
            }

            var updatedCompany = await mediator.Send(command);
            return Ok(updatedCompany);
        }
    }
}
