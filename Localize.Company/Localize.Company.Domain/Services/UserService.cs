using Localize.Company.Domain.Contracts.Repositories;
using Localize.Company.Domain.Contracts.Services;
using Localize.Company.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Localize.Company.Domain.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUserRepository repository, IHttpContextAccessor httpContextAccessor) : base(repository)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetByToken()
        {
            int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString());

            return _repository.Get(userId);
        }
    }
}
