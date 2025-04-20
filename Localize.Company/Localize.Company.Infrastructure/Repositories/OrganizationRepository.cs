using Localize.Company.Domain.Contracts.Repositories;
using Localize.Company.Domain.Entities;
using Localize.Company.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Localize.Company.Infrastructure.Repositories
{
    public class OrganizationRepository : RepositoryBase<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(LocalizeCompanyContext context) : base(context)
        {
        }

        public async Task<Organization> GetByCnpj(string cnpj)
        {
            return await _context.Organizations
                .Include(o => o.Endereco)
                .FirstOrDefaultAsync(o => o.Cnpj == cnpj);
        }

        public async Task<IEnumerable<Organization>> GetAllLoggedUser(int userId)
        {
            return await _context.Organizations
                .Include(o => o.Endereco)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
    }
}
