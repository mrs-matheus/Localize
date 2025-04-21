using Localize.Company.Application.Utils;
using Localize.Company.Domain.Entities;

namespace Localize.Company.Domain.Contracts.Repositories
{
    public interface IOrganizationRepository : IRepositoryBase<Organization>
    {
        Task<Organization> GetByCnpj(string cnpj);
        Task<PagedResult<Organization>> GetAllLoggedUser(int userId, int page, int pageSize);
    }
}
