using Localize.Company.Domain.Entities;

namespace Localize.Company.Domain.Contracts.Services
{
    public interface IOrganizationService : IServiceBase<Organization>
    {
        Task<Organization> GetByCnpj(string cnpj);
        Task AddByLoggedUser(Organization organization);
        Task<IEnumerable<Organization>> GetAllLoggedUser(int userId);
    }
}
