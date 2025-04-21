using Localize.Company.Domain.Contracts.Repositories;
using Localize.Company.Domain.Contracts.Services;
using Localize.Company.Domain.Entities;
using Localize.Company.Domain.Notifications;

namespace Localize.Company.Domain.Services
{
    public class OrganizationService : ServiceBase<Organization>, IOrganizationService
    {
        private readonly IOrganizationRepository _repository;
        private readonly IUserService _userService;
        public OrganizationService(
            IOrganizationRepository repository,
            NotificationContext notificationContext,
            IUserService userService) : base(repository, notificationContext)
        {
            _repository = repository;
            _userService = userService;
        }

        public async Task<Organization> GetByCnpj(string cnpj)
        {
            return await _repository.GetByCnpj(cnpj);
        }

        public async Task AddByLoggedUser(Organization organization)
        {
            var loggedUser = await _userService.GetByToken();
            organization.UserId = loggedUser.Id;
            await _repository.Add(organization);
        }

        public async Task<IEnumerable<Organization>> GetAllLoggedUser(int userId)
        {
            return await _repository.GetAllLoggedUser(userId);
        }
    }
}
