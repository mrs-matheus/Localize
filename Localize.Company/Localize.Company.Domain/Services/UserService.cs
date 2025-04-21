using Localize.Company.Domain.Contracts.Repositories;
using Localize.Company.Domain.Contracts.Services;
using Localize.Company.Domain.Entities;
using Localize.Company.Domain.Notifications;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Localize.Company.Domain.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(
            IUserRepository repository,
            IHttpContextAccessor httpContextAccessor,
            NotificationContext notificationContext) : base(repository, notificationContext)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetByToken()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null || !httpContext.User.Identity.IsAuthenticated)
                return null;

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
                return null;

            return _repository.Get(userId);
        }
    }
}
