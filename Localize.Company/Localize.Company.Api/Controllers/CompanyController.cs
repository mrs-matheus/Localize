using Localize.Company.Api.Requests;
using Localize.Company.Api.Validations;
using Localize.Company.Application.Contracts;
using Localize.Company.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Localize.Company.Api.Controllers
{
    [Authorize]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        [Route("cnpj/{cnpj}")]
        public async Task<IActionResult> AddCompany([FromRoute][ValidCnpj] string cnpj)
        {
            var result = await _companyService.AddCompany(cnpj);

            if(result.Success == false && result.Message.Contains("ReceitaWS-Error"))
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, result);
            }

            if (result.Success == false && result.Message.Equals("Register-Company-Error"))
            {
                return BadRequest(result);
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByLoggedUser([FromQuery] PagedRequest request)
        {
            var result = await _companyService.GetAllByLoggedUser(request.Page, request.PageSize);
            return Ok(result);
        }
    }
}
