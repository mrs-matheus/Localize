using AutoMapper;
using Localize.Company.Application.Contracts;
using Localize.Company.Application.DTOs;
using Localize.Company.Application.Responses;
using Localize.Company.Domain.Contracts.Services;
using Localize.Company.Domain.Entities;
using Localize.Company.Domain.Notifications;
using Localize.Company.Infrastructure.External.ReceitaWSApi.Contracts;

namespace Localize.Company.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationService _organizationService;
        private readonly IReceitaWSService _receitaWSService;
        private readonly IUserService _userService;
        private readonly NotificationContext _notification;
        public CompanyService(
            IOrganizationService organizationService,
            IReceitaWSService receitaWSService,
            IMapper mapper,
            IUserService userService,
            NotificationContext notification)
        {
            _organizationService = organizationService;
            _receitaWSService = receitaWSService;
            _mapper = mapper;
            _userService = userService;
            _notification = notification;
        }

        public async Task<ResponseBase<object>> AddCompany(string cnpj)
        {
            var existingCompany = await _organizationService.GetByCnpj(cnpj);

            if (existingCompany != null)
            {
                _notification.AddNotification("Cnpj", "Unable to register this company, try other");
                return ResponseBase<object>.Fail(_notification.Notifications.ToList());
            }

            var receitaWS = await _receitaWSService.GetCompanyByCnpj(cnpj);

            if (receitaWS == null)
            {
                return ResponseBase<object>.Fail(_notification.Notifications.ToList());
            }

            var organization = _mapper.Map<Organization>(receitaWS);

            await _organizationService.AddByLoggedUser(organization);

            return ResponseBase<object>.Ok();
        }

        public async Task<ResponseBase<OrganizationDto>> GetAllByLoggedUser()
        {
            var user = await _userService.GetByToken();

            var organizations = await _organizationService.GetAllLoggedUser(user.Id);

            return ResponseBase<OrganizationDto>.Ok(new OrganizationDto
            {
                User = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                },
                Organizations = organizations
            });
        }
    }
}
