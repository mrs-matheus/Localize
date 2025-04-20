using Localize.Company.Domain.Entities;

namespace Localize.Company.Domain.Contracts.Services
{
    public interface IUserService : IServiceBase<User>
    {
        Task<User> GetByToken();
    }
}
