using AutoMapper;
using Localize.Company.Application.Contracts;
using Localize.Company.Application.DTOs;
using Localize.Company.Domain.Contracts.Services;
using Localize.Company.Domain.Entities;
using Localize.Company.Infrastructure.External.ReceitaWSApi.Contracts;

namespace Localize.Company.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationService _organizationService;
        private readonly IReceitaWSService _receitaWSService;
        private readonly IUserService _userService;
        public CompanyService(
            IOrganizationService organizationService,
            IReceitaWSService receitaWSService,
            IMapper mapper,
            IUserService userService)
        {
            _organizationService = organizationService;
            _receitaWSService = receitaWSService;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task AddCompany(string cnpj)
        {
            var existingCompany = await _organizationService.GetByCnpj(cnpj);

            if (existingCompany != null)
            {
                throw new InvalidOperationException("Company with this CNPJ already exists");
            }

            var receitaWS = await _receitaWSService.GetCompanyByCnpj(cnpj);

            var organization = _mapper.Map<Organization>(receitaWS);

            await _organizationService.AddByLoggedUser(organization);
        }

        public async Task<OrganizationDto> GetAllByLoggedUser()
        {
            var user = await _userService.GetByToken();

            var organizations = await _organizationService.GetAllLoggedUser(user.Id);

            var organizationDto = new OrganizationDto
            {
                User = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                },
                Organizations = organizations
            };

            return organizationDto;
        }
    }
}
