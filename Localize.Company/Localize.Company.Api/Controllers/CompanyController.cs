using Localize.Company.Api.Requests;
using Localize.Company.Api.Validations;
using Localize.Company.Application.Contracts;
using Localize.Company.Application.DTOs;
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
            await _companyService.AddCompany(cnpj);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByLoggedUser()
        {
            var result = await _companyService.GetAllByLoggedUser();
            return Ok(result);
        }
    }
}
