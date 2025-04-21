using Localize.Company.Domain.Notifications;
using Localize.Company.Infrastructure.External.ReceitaWSApi.Contracts;
using Localize.Company.Infrastructure.External.ReceitaWSApi.Entities;

namespace Localize.Company.Infrastructure.External.ReceitaWSApi.Services
{
    public class ReceitaWSService : IReceitaWSService 
    {
        private readonly IReceitaWSRepository _repository;
        private readonly NotificationContext _notification;
        public ReceitaWSService(IReceitaWSRepository repository, NotificationContext notification)
        {
            _repository = repository;
            _notification = notification;
        }

        public async Task<ReceitaWS?> GetCompanyByCnpj(string cnpj)
        {
            return await _repository.GetCompanyByCnpj(cnpj);
        }
    }
}
