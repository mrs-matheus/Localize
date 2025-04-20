using Localize.Company.Infrastructure.External.ReceitaWSApi.Contracts;
using Localize.Company.Infrastructure.External.ReceitaWSApi.Entities;

namespace Localize.Company.Infrastructure.External.ReceitaWSApi.Services
{
    public class ReceitaWSService : IReceitaWSService
    {
        private readonly IReceitaWSRepository _repository;
        public ReceitaWSService(IReceitaWSRepository repository)
        {
            _repository = repository;
        }

        public async Task<ReceitaWS> GetCompanyByCnpj(string cnpj)
        {
            var result = await _repository.GetCompanyByCnpj(cnpj);

            if (result == null)
            {
                throw new Exception("Company not found");
            }

            return result;
        }
    }
}
