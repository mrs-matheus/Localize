using Localize.Company.Domain.Entities;

namespace Localize.Company.Domain.Contracts.Repositories
{
    public interface IOrganizationRepository : IRepositoryBase<Organization>
    {
        Task<Organization> GetByCnpj(string cnpj);
        Task<IEnumerable<Organization>> GetAllLoggedUser(int userId);
    }
}
