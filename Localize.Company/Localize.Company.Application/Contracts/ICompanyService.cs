using Localize.Company.Application.DTOs;
using Localize.Company.Application.Responses;

namespace Localize.Company.Application.Contracts
{
    public interface ICompanyService
    {
        Task<ResponseBase<object>> AddCompany(string cnpj);
        Task<ResponseBase<OrganizationDto>> GetAllByLoggedUser();
    }
}
