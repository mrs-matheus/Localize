using Localize.Company.Application.DTOs;

namespace Localize.Company.Application.Contracts
{
    public interface ICompanyService
    {
        Task AddCompany(string cnpj);
        Task<OrganizationDto> GetAllByLoggedUser();
    }
}
