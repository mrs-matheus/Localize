using Localize.Company.Application.Utils;
using Localize.Company.Domain.Contracts.Repositories;
using Localize.Company.Domain.Entities;
using Localize.Company.Domain.Notifications;
using Localize.Company.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Localize.Company.Infrastructure.Repositories
{
    public class OrganizationRepository : RepositoryBase<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(LocalizeCompanyContext context, NotificationContext notificationContext) : base(context, notificationContext)
        {
        }

        public async Task<Organization> GetByCnpj(string cnpj)
        {
            return await _context.Organizations
                .Include(o => o.Endereco)
                .FirstOrDefaultAsync(o => o.Cnpj == cnpj);
        }

        public async Task<PagedResult<Organization>> GetAllLoggedUser(int userId, int page, int pageSize)
        {
            var query = _context.Organizations
                .Include(x => x.Endereco)
                .Where(x => x.UserId == userId)
                .AsQueryable();

            var totalItems = await query.CountAsync();
            var items = await query.OrderBy(x => x.Id)
                .Skip(page - 1)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Organization>
            {
                Items = items,
                TotalItems = totalItems,
                CurrentPage = page,
                PageSize = pageSize
            };
        }
    }
}
