using Localize.Company.Domain.Contracts.Repositories;
using Localize.Company.Domain.Entities;
using Localize.Company.Domain.Notifications;
using Localize.Company.Infrastructure.Contexts;

namespace Localize.Company.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        protected LocalizeCompanyContext _context;
        public UserRepository(LocalizeCompanyContext context, NotificationContext notificationContext) : base(context, notificationContext) {}
    }
}
