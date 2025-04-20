using Localize.Company.Infrastructure.External.ReceitaWSApi.Entities;

namespace Localize.Company.Infrastructure.External.ReceitaWSApi.Contracts
{
    public interface IReceitaWSService
    {
        Task<ReceitaWS> GetCompanyByCnpj(string cnpj);
    }
}
